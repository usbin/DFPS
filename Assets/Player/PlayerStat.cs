using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int Speed { get; set; }
    public bool JumpCooldown { get; set; }
    private void Start()
    {
        Speed = 1;
        JumpCooldown = true;
    }
    private void Update()
    {
        //¶¥¿¡ ´êÀ¸¸é Á¡ÇÁ ÄðÅ¸ÀÓ ÃÊ±âÈ­

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) JumpCooldown = true;
    }


    IEnumerator JumpCooltime()
    {
        float cool = 3.0f;
        JumpCooldown = false;
        while (cool > 0)
        {
            cool -= Time.deltaTime;
            yield return null;
        }
        JumpCooldown = true;
    }

}
