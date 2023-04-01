using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public static float CalculateInflictedDamage(int attackerAtk, int defenderDef)
    {
        return Mathf.Max(attackerAtk - defenderDef, 1);
    }
}
