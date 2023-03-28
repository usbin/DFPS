using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Events;

public class Player : LivingEntity
{
    public Camera ViewCamera { get => _viewCamera; }
    Camera _viewCamera;

    public enum ViewMode { FIRST_PERSON, TOPDOWN }

    

    public override void Start()
    {
        base.Start();
    }


    public void Update()
    {
    }

    public void OnViewModeChanged(Director.ViewModeChangedArgs args)
    {
        _viewCamera = args.ViewCamera;
    }



}
