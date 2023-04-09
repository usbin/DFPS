using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl_FirstPerson : BasePlayerControl
{
    Rigidbody _playerRigidbody;
    GameObject _headAndBody;
    //���� �����ӿ� ������ ����
    Vector3 _movement;          // �̵�
    Vector3 _deltaLookDegree;   // �þ�
    float _jumpForce;           // ����

    

    public PlayerControl_FirstPerson(Rigidbody playerRigidbody, GameObject headAndBody)
    {
        _playerRigidbody = playerRigidbody;
        _headAndBody = headAndBody;
    }

    public override void Update(PlayerController.ControlArgs args)
    {
        PlayerInput input = args.Input;
        Player player = args.Player;
        PlayerController controller = args.PlayerController;

        // �̵�
        //����ĳ��Ʈ�� �ϴ� üũ
        Vector2 moveInput = input.actions["Move"].ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Quaternion rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0);
        Vector3 movement = (rotation * moveDirection) * Time.deltaTime * (1 + player.Speed);
        Ray moveRay = new Ray(player.transform.position, movement.normalized);
        RaycastHit hit;
        LayerMask layerMask = 1<<LayerMask.NameToLayer("Item");
        if(!Physics.Raycast(moveRay, out hit, 1, ~layerMask, QueryTriggerInteraction.Ignore))
        {
            Move(movement);
        }

        // �þ�
        InputActionMap playerActionMap = input.actions.FindActionMap("Player");
        if (input.currentActionMap == playerActionMap)
        {
            Vector2 lookInputDelta = input.actions["Look"].ReadValue<Vector2>();
            Vector3 lookDeltaDegree = new Vector3(-lookInputDelta.y, lookInputDelta.x, 0)*0.5f;

            Rotate(lookDeltaDegree);
        }


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
        _deltaLookDegree += deltaDegree;
    }
    void Move(Vector3 movement)
    {
        _movement += movement;
    }
    void Jump()
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
        //�������: -90��(=270��)<->70��
        //���� 90�������� �ø� �� �ְ�
        //�Ʒ��� 70�������� ���� �� ����.
        Vector3 wholeNewDegree = _playerRigidbody.transform.rotation.eulerAngles + _deltaLookDegree;
        Vector3 headAndBodyNewDegree = _headAndBody.transform.rotation.eulerAngles + _deltaLookDegree;
        if (headAndBodyNewDegree.x < 270 && headAndBodyNewDegree.x > 70)
        {
            //��������� �ƴ� ��
            if (_deltaLookDegree.x < 0)//���� �� ��
            {
                _deltaLookDegree.x = _playerRigidbody.transform.rotation.eulerAngles.x - 270f;
                headAndBodyNewDegree.x = 270;
            }
            else
            {
                _deltaLookDegree.x = _playerRigidbody.transform.rotation.eulerAngles.x - 70f;
                headAndBodyNewDegree.x = 70;
            }
        }
        _playerRigidbody.transform.rotation = Quaternion.Euler(_playerRigidbody.transform.rotation.eulerAngles.x, wholeNewDegree.y, wholeNewDegree.z);
        _headAndBody.transform.rotation = Quaternion.Euler(headAndBodyNewDegree.x, _headAndBody.transform.rotation.eulerAngles.y, _headAndBody.transform.rotation.eulerAngles.z);
        //_playerRigidbody.transform.eulerAngles = newDegree;
        _deltaLookDegree = Vector3.zero;

        //=====================
        // ����
        //=====================
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _jumpForce = 0;


        //======================
        // ������ ����
        //======================
        if(Vector3.Dot(_playerRigidbody.transform.up, Vector3.up) < 0)
        {
            _playerRigidbody.transform.rotation = Quaternion.identity;
        }
    }

}
