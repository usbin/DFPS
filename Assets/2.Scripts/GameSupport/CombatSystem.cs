using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CombatSystem : MonoBehaviour
{
    static CombatSystem _instance;
    public static CombatSystem Instance
    {
        get {
            if(_instance == null)
            {
                _instance = FindObjectOfType<CombatSystem>();
            }
            return _instance;
        }
    }
   

    public static int CalculateInflictedDamage(int attackerAtk, int defenderDef)
    {
        return Mathf.Max(attackerAtk - defenderDef, 1);
    }
}
