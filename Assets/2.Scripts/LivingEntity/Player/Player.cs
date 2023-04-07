using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerBuffController))]
[RequireComponent(typeof(PlayerSkillController))]
public class Player : LivingEntity
{
    public Camera ViewCamera { get => _viewCamera; }
    public BaseSkill.SkillManager SkillManager = new BaseSkill.SkillManager();


    Camera _viewCamera;
    PlayerSkillController _skillController;
    PlayerBuffController _buffController;

    public enum ViewMode { FIRST_PERSON, TOPDOWN }

    private void Awake()
    {
        _skillController = GetComponent<PlayerSkillController>();
        _buffController = GetComponent<PlayerBuffController>();
    }
    public override void Start()
    {
        base.Start();
    }


    public override void Update()
    {
        base.Update();
    }


    //=====================
    // 스킬 컨트롤러가 담당
    //=====================
    public BaseSkill QSkill => _skillController.QSkill;
    public BaseSkill ESkill => _skillController.ESkill;
    public BaseSkill RSkill => _skillController.RSkill;
    public PlayerSkillController.OnSkillChangedHandler OnSkillChanged
    {
        get { return _skillController.OnSkillChanged; }
        set { _skillController.OnSkillChanged = value; }
    }
    //공격도 스킬에 포함
    public override void ExecuteSkill(BaseSkill skill, SkillArgs args)
    {
        StartCoroutine(SkillManager.ExecuteSkill(skill, args, SkillManager));
    }

    //스킬 습득 함수
    public void SetupSkill(BaseSkill skill_, string key)
    {
        _skillController.SetupSkill(SkillManager, skill_, key);
    }
    //스킬 습득 취소 함수
    public void SetdownSkill(BaseSkill skill)
    {
        _skillController.SetdownSkill(SkillManager, skill);
    }
    //=====================
    // 버프 컨트롤러가 담당
    //=====================

    public event System.Action<BaseBuff> OnBuffAdded
    {
        add
        {
            _buffController.OnBuffAdded += value;
        }
        remove
        {
            _buffController.OnBuffAdded -= value;
        }
    }
    public event System.Action<BaseBuff> OnBuffRemoved
    {
        add
        {
            _buffController.OnBuffRemoved += value;
        }
        remove
        {
            _buffController.OnBuffRemoved -= value;
        }
    }
    public override BaseBuff[] AllActiveBuff => _buffController.AllActiveBuff;
    public override void Affect(BaseBuff buff)
    {
        _buffController.Affect(buff);
    }

    public void OnViewModeChanged(ModeSwitcher.ViewModeChangedArgs args)
    {
        _viewCamera = args.ViewCamera;
    }


}
