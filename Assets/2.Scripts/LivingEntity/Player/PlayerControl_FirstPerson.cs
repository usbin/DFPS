using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl_FirstPerson : BasePlayerControl
{
    Rigidbody _playerRigidbody;

    //다음 프레임에 적용할 값들
    Vector3 _movement;          // 이동
    Vector3 _deltaLookDegree;   // 시야
    float _jumpForce;           // 점프

    

    public PlayerControl_FirstPerson(Rigidbody playerRigidbody)
    {
        _playerRigidbody = playerRigidbody;
    }

    public override void Update(PlayerController.ControlArgs args)
    {
        PlayerInput input = args.Input;
        Player player = args.Player;
        PlayerController controller = args.PlayerController;

        // 이동
        //레이캐스트로 일단 체크
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

        // 시야
        InputActionMap playerActionMap = input.actions.FindActionMap("Player");
        if (input.currentActionMap == playerActionMap)
        {
            Vector2 lookInputDelta = input.actions["Look"].ReadValue<Vector2>();
            /*            Vector2 oldLookPositionOnScreen = player.ViewCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
                        Vector2 currentLookPositionOnScreen = oldLookPositionOnScreen + lookInputDelta;
                        //originLookPosition -> currentLookPosition으로 이동했을 때 돌려야 하는 각도
                        // 적당한 거리의, forward에 수직인 plane을 정의하고 여기에 부딪힌 거리를 잼.
                        Plane plane = new Plane(player.ViewCamera.transform.forward, player.ViewCamera.transform.position + player.ViewCamera.transform.forward*10);
                        Ray currentLookRay = player.ViewCamera.ScreenPointToRay(currentLookPositionOnScreen);
                        Ray oldLookRay = player.ViewCamera.ScreenPointToRay(oldLookPositionOnScreen);
                        float currentLookDistance;
                        float oldLookDistance;
                        Debug.DrawRay(currentLookRay.origin, currentLookRay.direction, Color.red);
                       if( plane.Raycast(currentLookRay, out currentLookDistance) && plane.Raycast(oldLookRay, out oldLookDistance))
                        {
                            Vector3 currentLookPosition = currentLookRay.GetPoint(currentLookDistance);
                            Vector3 oldLookPosition = oldLookRay.GetPoint(oldLookDistance);
                            Vector3 currentLookDistance3d = currentLookPosition-currentLookRay.origin;
                            Vector3 oldLookDistance3d = oldLookPosition-oldLookRay.origin;
                            Vector3 deltaDistance3d = currentLookPosition - oldLookPosition;


                            //큰 int값으로
                            int currentDist3dX = (int)(currentLookDistance3d.x * 100f);
                            int currentDist3dY = (int)(currentLookDistance3d.y * 100f);
                            int currentDist3dZ = (int)(currentLookDistance3d.z * 100f);
                            int oldDist3dX = (int)(oldLookDistance3d.x * 100f);
                            int oldDist3dY = (int)(oldLookDistance3d.y * 100f);
                            int oldDist3dZ = (int)(oldLookDistance3d.z * 100f);
                            int deltaDist3dX = (int)(deltaDistance3d.x * 100f);
                            int deltaDist3dY = (int)(deltaDistance3d.y * 100f);
                            int deltaDist3dZ = (int)(deltaDistance3d.z * 100f);

                            int cosThetaX = 0;
                            if ((2 * currentDist3dX * oldDist3dX/10000) != 0)
                            {
                                cosThetaX = ((currentDist3dX * currentDist3dX) / 10000
                                    + (oldDist3dX * oldDist3dX) / 10000
                                    - (deltaDist3dX * deltaDist3dX) / 10000
                                    ) / ((2 * currentDist3dX * oldDist3dX) / 10000);
                            }


                            int cosThetaY = 0;
                            if(2 * currentDist3dY * oldDist3dY / 10000 != 0)
                            {
                                cosThetaY = ((currentDist3dY * currentDist3dY) / 10000
                                    + (oldDist3dY * oldDist3dY) / 10000
                                    - (deltaDist3dY * deltaDist3dY) / 10000
                                    ) / (2 * currentDist3dY * oldDist3dY / 10000);
                            }

                            int cosThetaZ = 0;
                            if((2 * currentDist3dZ * oldDist3dZ / 10000) != 0)
                                cosThetaZ = ((currentDist3dZ* currentDist3dZ) / 10000
                                    + (oldDist3dZ* oldDist3dZ) / 10000
                                    - (deltaDist3dZ* deltaDist3dZ) / 10000
                                    ) / (2 * currentDist3dZ * oldDist3dZ / 10000);

                            Vector3 theta = new Vector3(Mathf.Acos(cosThetaX), Mathf.Acos(cosThetaY), Mathf.Acos(cosThetaZ))*0.1f;
                            Rotate(theta);
                        }*/

            Vector3 lookDeltaDegree = new Vector3(-lookInputDelta.y, lookInputDelta.x, 0)*0.5f;

            Rotate(lookDeltaDegree);
        }


        // 점프
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
        // 이동
        //=====================
        _playerRigidbody.MovePosition
            (_playerRigidbody.position + _movement);
        _movement = Vector3.zero;


        //=====================
        // 시야
        //=====================
        //가용범위: -90도(=270도)<->70도
        //위쪽 90도까지만 올릴 수 있고
        //아래쪽 70도까지만 숙일 수 있음.
        Vector3 newDegree = _playerRigidbody.transform.rotation.eulerAngles + _deltaLookDegree;
        if (newDegree.x < 270 && newDegree.x > 70)
        {
            //가용범위가 아닐 때
            if (_deltaLookDegree.x < 0)//위를 볼 때
            {
                _deltaLookDegree.x = _playerRigidbody.transform.rotation.eulerAngles.x - 270f;
                newDegree.x = 270;
            }
            else
            {
                _deltaLookDegree.x = _playerRigidbody.transform.rotation.eulerAngles.x - 70f;
                newDegree.x = 70;
            }
        }
        _playerRigidbody.transform.rotation = Quaternion.Euler(newDegree);
        //_playerRigidbody.transform.eulerAngles = newDegree;
        _deltaLookDegree = Vector3.zero;

        //=====================
        // 점프
        //=====================
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _jumpForce = 0;


    }

}
