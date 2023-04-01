using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Player Player;
    public Camera MinimapCamera;
    public Transform PlayerOnMinimap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            MinimapCamera.transform.position = new Vector3(Player.transform.position.x,
                MinimapCamera.transform.position.y,
                Player.transform.position.z);
            PlayerOnMinimap.transform.rotation = Quaternion.Euler(
                90,
                Player.transform.rotation.eulerAngles.y,
                0);   //ī�޶� ȸ���������Ƿ� y���� �ƴ� z���� ���ʹ�����
        }
    }
}
