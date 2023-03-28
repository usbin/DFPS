using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl_FirstPerson : PlayerControl
{
    Rigidbody _playerRigidbody;

    //���� �����ӿ� ������ ����
    Vector3 _movement;          // �̵�
    Vector3 _deltaLookDegree;   // �þ�
    float _jumpForce;           // ����

    public PlayerControl_FirstPerson(Rigidbody playerRigidbody)
    {
        _playerRigidbody = playerRigidbody;
    }

    public override void Update(PlayerController.ControlArgs args)
    {
        PlayerInput input = args.Input;
        Player player = args.Player;
        PlayerController controller = args.PlayerController;

        // �̵�
        Vector2 moveInput = input.actions["Move"].ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Quaternion rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0);
        Vector3 movement = (rotation * moveDirection) * Time.deltaTime * (1 + player.Speed) * 5f;
        Move(movement);


        // �þ�
        Vector2 lookInputDelta = input.actions["Look"].ReadValue<Vector2>();
        Vector3 lookDeltaDegree = new Vector3(-lookInputDelta.y, lookInputDelta.x, 0) * 0.05f;
        Rotate(lookDeltaDegree);

        // ����
        if (input.actions["Jump"].ReadValue<float>() > 0
            && controller.JumpCooldown)
        {
            Jump();
            controller.JumpCooldown = false;
        }
    }
    void Rotate(Vector3 deltaDegree)
    {
        _deltaLookDegree += deltaDegree * 15f;
    }
    void Move(Vector3 movement)
    {
        _movement += movement;
    }
    void Jump()
    {
        _jumpForce += Mathf.Sqrt(2 * _playerRigidbody.mass * Physics.gravity.magnitude * 1);
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
        //�������: -90��(=270��)<->70��
        //���� 90�������� �ø� �� �ְ�
        //�Ʒ��� 70�������� ���� �� ����.
        Vector3 newDegree = _playerRigidbody.transform.rotation.eulerAngles + _deltaLookDegree;
        if (newDegree.x < 270 && newDegree.x > 70)
        {
            //��������� �ƴ� ��
            if (_deltaLookDegree.x < 0) newDegree.x = 270;
            else newDegree.x = 70;
        }
        _playerRigidbody.transform.eulerAngles = newDegree;
        _deltaLookDegree = Vector3.zero;

        //=====================
        // ����
        //=====================
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _jumpForce = 0;


    }

}
