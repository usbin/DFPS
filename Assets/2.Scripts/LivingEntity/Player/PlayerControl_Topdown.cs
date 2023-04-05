using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl_Topdown : BasePlayerControl
{
    Rigidbody _playerRigidbody;

    //���� �����ӿ� ������ ����
    bool _lookAtDirty = false;
    Vector3 _movement;          // �̵�
    Vector3 _lookAtPos;         // �þ�
    float _jumpForce;           // ����

    public PlayerControl_Topdown(Rigidbody playerRigidbody)
    {
        _playerRigidbody = playerRigidbody;

    }

    public override void Update(PlayerController.ControlArgs args)
    {
        PlayerInput input = args.Input;
        Player player = args.Player;
        PlayerController controller = args.PlayerController;

        // �̵� : ī�޶󿡼� �� �������� ������ ȣ��
        Vector2 moveInput = input.actions["Move"].ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Quaternion rotation = Quaternion.Euler(0, player.ViewCamera.transform.rotation.eulerAngles.y, 0);
        Vector3 movement = (rotation * moveDirection) * Time.deltaTime * (1 + player.Speed);
        // ray�� �浹�˻�
        Ray moveRay = new Ray(player.transform.position, movement.normalized);
        RaycastHit hit;
        LayerMask layerMask = 1<<LayerMask.NameToLayer("Item");

        if(!Physics.Raycast(moveRay,out hit, 1, ~layerMask, QueryTriggerInteraction.Ignore))
        {
            Move(movement);
        }

        // �þ� : ���콺 ���� �ٶ󺸱�(�Է��� ���� ����)
        InputActionMap playerActionMap = input.actions.FindActionMap("Player");
        if(input.currentActionMap == playerActionMap)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = player.ViewCamera.ScreenPointToRay(mousePos);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 point = ray.GetPoint(distance);
                point.y = player.transform.position.y;
                LookAt(point);
            }
        }
        

        // ����
        if (input.actions["Jump"].ReadValue<float>() > 0
            && controller.JumpCooldown)
        {
            Jump();
            controller.JumpCooldown = false;
        }
    }


    public void LookAt(Vector3 position)
    {
        _lookAtPos = position;
        _lookAtDirty = true;
    }
    public void Move(Vector3 movement)
    {
        _movement += movement;
    }
    public void Jump()
    {
        _jumpForce += Mathf.Sqrt(2 * _playerRigidbody.mass * Physics.gravity.magnitude * 2);
    }

    public override void ApplyChange()
    {
        //=====================
        // �̵�
        //=====================
        _playerRigidbody.MovePosition
            (_playerRigidbody.position + _movement);
        _movement = Vector3.zero;


        //=====================
        // �þ�
        //=====================
        if (_lookAtDirty)
        {
            _playerRigidbody.transform.LookAt(_lookAtPos);
            _lookAtDirty = false;

        }

        //=====================
        // ����
        //=====================
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _jumpForce = 0;


    }


}
