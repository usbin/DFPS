using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public static float CalculateInflictedDamage(ICombatable attacker, Weapon attackerWeapon, IDamagable target)
    {
        return Mathf.Max(attacker.Atk + attackerWeapon.Atk - target.Def, 1);
    }
}
