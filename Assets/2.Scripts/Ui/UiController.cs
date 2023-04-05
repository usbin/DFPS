using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameObject AimPoint;
    public Slider PlayerHpBar;
    public Player Player;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //hp바 갱신
        PlayerHpBar.maxValue = Player.MaxHp;
        PlayerHpBar.value = Player.Hp;
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
            //조준점 보이기
            AimPoint.SetActive(true);
        }
        else
        {
            AimPoint.SetActive(false);
        }
    }
}
