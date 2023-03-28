using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_FirstPersonView : MonoBehaviour
{
    public Player Player;

    private void LateUpdate()
    {
        transform.position = Player.transform.position;
        transform.rotation = Player.transform.rotation;
    }
}
