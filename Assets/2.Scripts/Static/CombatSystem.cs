using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public static float CalculateInflictedDamage(ICombatable attacker, Weapon attackerWeapon, IDamagable target)
    {
        return Mathf.Max(attacker.Atk + attackerWeapon.Atk - target.Def, 1);
    }
    public static float CalculateInflictedDamage(CombatArgs args)
    {
        return Mathf.Max(args.AttackerAtk - args.DefenderDef, 1);
    }
}
