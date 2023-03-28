using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_TopdownView : MonoBehaviour
{
    public Player Player;

    void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
    }
}
