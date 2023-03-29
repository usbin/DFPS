using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LivingEntity))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Player _player;
    PlayerInput _input;
    Rigidbody _playerRigidbody;
    BasePlayerControl[] _controls = new BasePlayerControl[2];
    BasePlayerControl _currentControl;

    public bool JumpCooldown;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _input = GetComponent<PlayerInput>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _controls[(int)Player.ViewMode.FIRST_PERSON] = new PlayerControl_FirstPerson(_playerRigidbody);
        _controls[(int)Player.ViewMode.TOPDOWN] = new PlayerControl_Topdown(_playerRigidbody);
    }

    void Start()
    {


        
    }
    private void Update()
    {
        ControlArgs args;
        args.Input = _input;
        args.Player = _player;
        args.PlayerController = this;

        if(_currentControl != null) _currentControl.Update(args);
    }

    private void FixedUpdate()
    {
        if (_currentControl != null) _currentControl.ApplyChange();
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) JumpCooldown = true;
    }

    public void OnViewModeChanged(ModeSwitcher.ViewModeChangedArgs args)
    {
        _currentControl = _controls[(int)args.ViewMode];
    }

    public struct ControlArgs
    {
        public PlayerInput Input;
        public Player Player;
        public PlayerController PlayerController;
    }

}
