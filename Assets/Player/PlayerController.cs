using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody _playerRigidbody;


    bool _jumpCooldown = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 moveDirection, PlayerStat stat)
    {
        _playerRigidbody.MovePosition(
            _playerRigidbody.position
            + transform.TransformDirection(moveDirection.normalized) * 0.01f * stat.Speed
        );
        Vector3 movement = transform.rotation * moveDirection.normalized * 0.01f * stat.Speed;
        movement.y = 0;//���δ� �������� ����.
        _playerRigidbody.MovePosition
            (_playerRigidbody.position + movement);
    }
    public void Look(Vector3 lookDelta)
    {
        Vector3 oldDegree = transform.rotation.eulerAngles;
        Vector3 newDegree = oldDegree + lookDelta;

        //�������: -90��(=270��)<->70��
        //���� 90�������� �ø� �� �ְ�
        //�Ʒ��� 70�������� ���� �� ����.
        if(newDegree.x<270 && newDegree.x > 70)
        {
            //��������� �ƴ� ��
            if (lookDelta.y < 0) newDegree.x = 270;
            else newDegree.x = 70;
        }
        
        transform.eulerAngles = newDegree;
    }
    public void Jump()
    {
        if (_jumpCooldown)
        {
            _playerRigidbody.MovePosition(_playerRigidbody.position + new Vector3(0, 1, 0));
            StartCoroutine(JumpCooltime(0.5f));
        }
    }

    IEnumerator JumpCooltime (float cool)
    {
        _jumpCooldown = false;
        while (cool > 0)
        {
            cool -= Time.deltaTime;
            yield return null;
        }
        _jumpCooldown = true;
    }
    
}
