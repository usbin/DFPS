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

    public BaseSkill.SkillManager SkillExecutor => _skillExecutor;
    BaseSkill.SkillManager _skillExecutor = new BaseSkill.SkillManager();
    
    public void ExecuteSkill(BaseSkill skill, SkillArgs args)
    {
        StartCoroutine(_skillExecutor.ExecuteSkill(skill, args, _skillExecutor));

    }


    public static int CalculateInflictedDamage(int attackerAtk, int defenderDef)
    {
        return Mathf.Max(attackerAtk - defenderDef, 1);
    }
}
public struct AttackArgs
{
    public ICombatable Attacker;
    public Weapon Weapon;       //nullable
    public List<ICombatable> Targets;
    public AttackType Type;
    public int BaseStat;        //1:1������ ���� ����
    public bool Intercepted;    //���ͼ�Ʈ �� �� �ִ°�
}

public enum AttackType
{
    NormalAttack,
    SkillAttack,
    END                      //�нú� ��ų � ���� �߰� ����
}
public delegate void AttackInterceptor(ref AttackArgs args);
public delegate int DamageInterceptor(int Damage);

public struct DamageArgs
{
    public int Damage;
    public ICombatable Target;
}

