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


    Vector2 prevMouseDownPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _viewCamera = Camera.main;
        _stat = GetComponent<PlayerStat>();
        _controller = GetComponent<PlayerController>();
        _input = GetComponent<PlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        //시야 회전
        Vector2 lookInputDelta = _input.actions["Look"].ReadValue<Vector2>();
        _controller.Look(new Vector3(-lookInputDelta.y * 0.1f, lookInputDelta.x * 0.1f, 0));

        //이동
        Vector2 moveInput = _input.actions["Move"].ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        _controller.Move(movement * Time.deltaTime, _stat);

        //점프
        if (_input.actions["Jump"].ReadValue<float>() > 0) _controller.Jump();



    }
    private void FixedUpdate()
    {

        /*//조준점
        Ray ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
        }*/

        
        
    }

}
