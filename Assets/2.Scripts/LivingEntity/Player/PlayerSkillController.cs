using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Player))]
public class PlayerSkillController : MonoBehaviour
{
    public CombatSystem CombatSystem;
    public Transform SkillBelt;
    Dictionary<string, BaseSkill> _mySkills = new Dictionary<string, BaseSkill>();
    Player _player;
    PlayerInput _input;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _input = GetComponent<PlayerInput>();
    }

    //스킬 습득 함수
    public void SetupSkill(BaseSkill skill_, string key)
    {
        BaseSkill skill = Instantiate(skill_);
        skill.transform.SetParent(SkillBelt);
        if(skill.OnSetup != null) skill.OnSetup(_player, CombatSystem.SkillExecutor);
        _mySkills.Add(key, skill);
    }
    //스킬 습득 취소 함수
    public void SetdownSkill(BaseSkill skill)
    {
        foreach(string key in _mySkills.Keys)
        {
            if (_mySkills[key] == skill)
            {
                _mySkills.Remove(key);
                Destroy(skill.gameObject);
                return;
            }
        }
    }
    public void OnPressQ(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPressSkillKey("Q");
        }
    }
    public void OnPressE(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPressSkillKey("E");
        }
    }
    public void OnPressR(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPressSkillKey("R");
        }
    }
    void OnPressSkillKey(string key)
    {
        if (_mySkills.ContainsKey(key))
        {
            SkillArgs args;
            args.Caster = _player;
            args.Origin = _player.transform.position;
            args.Direction = _player.transform.forward;
            args.Weapon = null;
            CombatSystem.Instance.ExecuteSkill(_mySkills[key], args);

        }
    }
}
