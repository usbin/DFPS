using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_FirstPersonView : MonoBehaviour
{
    public GameObject PlayerHead;

    private void LateUpdate()
    {
        if (PlayerHead)
        {
            transform.position = new Vector3(
                PlayerHead.transform.position.x,
                PlayerHead.transform.position.y + 1,
                PlayerHead.transform.position.z
                );
            transform.rotation = PlayerHead.transform.rotation;

        }
    }
}
