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
    public int Atk;
    public abstract bool NormalAttack(WeaponAttackArgs args);
}

public struct WeaponAttackArgs
{
    public LivingEntity Attacker;
    public Vector3 Origin;
    public Vector3 Direction;
}