using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody _playerRigidbody;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 moveDirection, PlayerStat stat)
    {
        Vector3 movement = transform.rotation * moveDirection.normalized * 0.05f * stat.Speed;
        movement.y = 0;//���δ� �������� ����.
        _playerRigidbody.MovePosition
            (_playerRigidbody.position + movement);
    }
    public void Look(Vector3 deltaDegree)
    {
        Vector3 oldDegree = transform.rotation.eulerAngles;
        Vector3 newDegree = oldDegree + deltaDegree*0.5f;

        //�������: -90��(=270��)<->70��
        //���� 90�������� �ø� �� �ְ�
        //�Ʒ��� 70�������� ���� �� ����.
        if(newDegree.x<270 && newDegree.x > 70)
        {
            //��������� �ƴ� ��
            if (deltaDegree.x < 0) newDegree.x = 270;
            else newDegree.x = 70;
        }
        
        transform.eulerAngles = newDegree;
    }
    public void Jump()
    {
        float jumpForce = Mathf.Sqrt(2 * _playerRigidbody.mass * Physics.gravity.magnitude * 1);
        _playerRigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
    }

    

}
