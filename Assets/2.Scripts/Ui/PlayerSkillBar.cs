using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSkillBar : MonoBehaviour
{
    public Player Player;
    public Image QSkillImage;
    public Image QSkillCooltime;
    public TMPro.TextMeshProUGUI QSkillTooltip;
    public Image ESkillImage;
    public Image ESkillCooltime;
    public TMPro.TextMeshProUGUI ESkillTooltip;
    public Image RSkillImage;
    public Image RSkillCooltime;
    public TMPro.TextMeshProUGUI RSkillTooltip;


    private void Start()
    {

        Player.OnSkillChanged += OnSkillChanged;
    }
    void Update()
    {

        CooltimeUpdate();
    }

    void OnSkillChanged(string key, BaseSkill skill)
    {
        Sprite sprite = null;
        bool skillVisible = false;
        if (skill != null)
        {
            sprite = skill.SkillImage;
            skillVisible = true;
        }
        switch (key)
        {
            case "Q":
                QSkillImage.sprite = sprite;
                QSkillImage.gameObject.SetActive(skillVisible);
                QSkillTooltip.gameObject.SetActive(skillVisible);
                QSkillTooltip.text = skill.SkillDescription;
                break;
            case "E":
                ESkillImage.sprite = sprite;
                ESkillImage.gameObject.SetActive(skillVisible);
                ESkillTooltip.gameObject.SetActive(skillVisible);
                ESkillTooltip.text = skill.SkillDescription;
                break;
            case "R":
                RSkillImage.sprite = sprite;
                RSkillImage.gameObject.SetActive(skillVisible);
                RSkillTooltip.gameObject.SetActive(skillVisible);
                RSkillTooltip.text = skill.SkillDescription;
                break;
        }
        
    }
    void CooltimeUpdate()
    {
        //쿨타임이 없는 스킬이면 항상 0
        //쿨타임이 있는 스킬이면 남은 쿨타임/전체 쿨타임
        if(Player.QSkill != null)
            QSkillCooltime.fillAmount = Player.QSkill.RemainCooltime / Player.QSkill.Cooltime;
        if (Player.ESkill != null)
            ESkillCooltime.fillAmount = Player.ESkill.RemainCooltime / Player.ESkill.Cooltime;
        if (Player.RSkill != null)
            RSkillCooltime.fillAmount = Player.RSkill.RemainCooltime / Player.RSkill.Cooltime;
    }

    private void OnDestroy()
    {
        Player.OnSkillChanged -= OnSkillChanged;
    }
}
