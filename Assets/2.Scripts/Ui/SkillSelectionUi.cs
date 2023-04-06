using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SkillSelectionUi : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TitleText;
    public Button BtnFirstSkill;
    public Button BtnSecondSkill;
    public Button BtnThirdSkill;

    public void Init(string titleText, ModeSwitcher modeSwitcher, BaseSkill firstSkill, UnityAction onClickFirstSkill, 
        BaseSkill secondSkill, UnityAction onClickSecondSkill,
        BaseSkill thirdSkill, UnityAction onClickThirdSkill)
    {
        modeSwitcher.SetUiMode(true);
        BtnFirstSkill.onClick.RemoveAllListeners();
        BtnSecondSkill.onClick.RemoveAllListeners();
        BtnThirdSkill.onClick.RemoveAllListeners();

        TitleText.text = titleText;

        //��ų �̸�
        BtnFirstSkill.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = firstSkill.SkillName;
        BtnSecondSkill.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = secondSkill.SkillName;
        BtnThirdSkill.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = thirdSkill.SkillName;
        //��ų ����
        BtnFirstSkill.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = firstSkill.SkillDescription;
        BtnSecondSkill.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = secondSkill.SkillDescription;
        BtnThirdSkill.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = thirdSkill.SkillDescription;
        //��ų �̹���
        BtnFirstSkill.transform.GetChild(2).GetComponent<Image>().sprite = firstSkill.SkillImage;
        BtnSecondSkill.transform.GetChild(2).GetComponent<Image>().sprite = secondSkill.SkillImage;
        BtnThirdSkill.transform.GetChild(2).GetComponent<Image>().sprite = thirdSkill.SkillImage;


        //��ų Ŭ�� ������
        BtnFirstSkill.onClick.AddListener(onClickFirstSkill);
        BtnSecondSkill.onClick.AddListener(onClickSecondSkill);
        BtnThirdSkill.onClick.AddListener(onClickThirdSkill);

        //���� �����ϸ� â ����.
        UnityAction exit = () =>
        {
            modeSwitcher.SetUiMode(false);
            Destroy(gameObject);
        };
        BtnFirstSkill.onClick.AddListener(exit);
        BtnSecondSkill.onClick.AddListener(exit);
        BtnThirdSkill.onClick.AddListener(exit);

    }


}
