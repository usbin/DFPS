using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject AimPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAttack(Weapon weapon)
    {
        StartCoroutine(GunAttackEffect());
    }
    IEnumerator GunAttackEffect()
    {
        Vector3 orgScale = new Vector3(1, 1, 1);
        Vector3 effectScale = orgScale * 1.5f;
        AimPoint.transform.localScale = effectScale;

        float duration = 0.05f;
        while (duration>0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }

        AimPoint.transform.localScale = orgScale;


    }
    public void OnEquipWeapon(Weapon weapon)
    {
        if(weapon.Type == Weapon.WeaponType.Gun)
        {
            //������ ���̱�
            AimPoint.SetActive(true);
        }
        else
        {
            AimPoint.SetActive(false);
        }
    }
}
