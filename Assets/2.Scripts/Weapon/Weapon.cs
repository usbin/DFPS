using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public enum WeaponType
    {
        Gun
    }
    public WeaponType Type;
    public float Atk;
    public abstract void NormalAttack(AttackArgs args);
}
