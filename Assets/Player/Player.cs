using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;


[RequireComponent(typeof(PlayerStat))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    Camera _viewCamera;
    PlayerStat _stat;
    PlayerController _controller;
    PlayerInput _input;
    bool _mode3d = true;

    
    void Start()
    {
        _viewCamera = Camera.main;
        _stat = GetComponent<PlayerStat>();
        _controller = GetComponent<PlayerController>();
        _input = GetComponent<PlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        //�þ� ȸ��
        Vector2 lookInputDelta = _input.actions["Look"].ReadValue<Vector2>();
        _controller.Look(new Vector3(-lookInputDelta.y, lookInputDelta.x, 0));

        //�̵�
        Vector2 moveInput = _input.actions["Move"].ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        _controller.Move(movement, _stat);

        //����
        if (_input.actions["Jump"].ReadValue<float>() > 0
            && _stat.JumpCooldown) {
            _stat.JumpCooldown = false;
            _controller.Jump();
        }

        //������
        if (_mode3d) //3d ����� ��:ȭ�� �߾��� ������
        {
            Ray ray = _viewCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5f))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);
            }
        }
        else //ž�ٿ� ����� ��: ���콺 ��ġ�� ������
        {

        }
        
        



    }



}
