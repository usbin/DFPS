using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_FirstPersonView : MonoBehaviour
{
    public Player Player;

    private void LateUpdate()
    {
        if (Player)
        {
            Vector3 playerForword = Player.transform.forward;
            transform.position = new Vector3(
                Player.transform.position.x,
                Player.transform.position.y + 1,
                Player.transform.position.z
                );
            transform.rotation = Player.transform.rotation;

        }
    }
}
