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
        movement.y = 0;//위로는 움직이지 않음.
        _playerRigidbody.MovePosition
            (_playerRigidbody.position + movement);
    }
    public void Look(Vector3 deltaDegree)
    {
        Vector3 oldDegree = transform.rotation.eulerAngles;
        Vector3 newDegree = oldDegree + deltaDegree*0.5f;

        //가용범위: -90도(=270도)<->70도
        //위쪽 90도까지만 올릴 수 있고
        //아래쪽 70도까지만 숙일 수 있음.
        if(newDegree.x<270 && newDegree.x > 70)
        {
            //가용범위가 아닐 때
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
