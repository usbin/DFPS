using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Player player;

    private void LateUpdate()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
    }
}
