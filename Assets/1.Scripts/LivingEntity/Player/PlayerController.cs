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
    PlayerControl[] _controls = new PlayerControl[2];
    PlayerControl _currentControl;

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
        _currentControl.Update(args);
    }

    private void FixedUpdate()
    {
        _currentControl.ApplyChange();
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) JumpCooldown = true;
    }

    public void OnViewModeChanged(Director.ViewModeChangedArgs args)
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
