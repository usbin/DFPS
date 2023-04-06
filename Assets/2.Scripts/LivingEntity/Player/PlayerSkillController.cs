using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Player))]
public class PlayerSkillController : MonoBehaviour
{
    public delegate void OnSkillChangedHandler(string key, BaseSkill skill);
    public OnSkillChangedHandler OnSkillChanged;
    public Transform SkillBelt;
    public BaseSkill QSkill
    {
        get
        {
            if (_mySkills.ContainsKey("Q")) return _mySkills["Q"];
            else return null;
        }
    }
    public BaseSkill ESkill
    {
        get
        {
            if (_mySkills.ContainsKey("E")) return _mySkills["E"];
            else return null;
        }
    }
    public BaseSkill RSkill
    {
        get
        {
            if (_mySkills.ContainsKey("R")) return _mySkills["R"];
            else return null;
        }
    }
    Dictionary<string, BaseSkill> _mySkills = new Dictionary<string, BaseSkill>();
    Player _player;
    PlayerInput _input;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _input = GetComponent<PlayerInput>();
    }

    //스킬 습득 함수
    public void SetupSkill(BaseSkill.SkillManager skillManager, BaseSkill skill_, string key)
    {
        BaseSkill skill = Instantiate(skill_);
        skill.transform.SetParent(SkillBelt);

        skillManager.OnSetupSkill(skill_, _player);

        _mySkills.Add(key, skill);

        if (OnSkillChanged != null) OnSkillChanged(key, skill_);
    }
    //스킬 습득 취소 함수
    public void SetdownSkill(BaseSkill.SkillManager skillManager, BaseSkill skill)
    {
        skillManager.OnSetdownSkill(skill, _player);
        foreach (string key in _mySkills.Keys)
        {
            if (_mySkills[key] == skill)
            {
                _mySkills.Remove(key);
                if (OnSkillChanged != null) OnSkillChanged(key, null);
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
            _player.ExecuteSkill(_mySkills[key], args);

        }
    }
}
