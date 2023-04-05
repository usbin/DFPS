using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class BaseSkill : MonoBehaviour
{
    public string SkillName;
    public delegate void SetupHandler(LivingEntity owner, SkillManager skillExecutor);
    public delegate void SetdownHandler(LivingEntity owner, SkillManager skillExecutor);
    public delegate IEnumerator ExecuteHandler(SkillArgs args, SkillManager skillManager);
    public delegate IEnumerator TriggerHandler(SkillArgs args, SkillResult result, SkillManager skillManager);
    //SkillResult 반환

    public abstract SetupHandler OnSetup { get; }       //습득했을때
    public abstract SetdownHandler OnSetdown { get; }   //습득 취소했을 때
    public abstract ExecuteHandler OnExecute { get; }   //사용했을 때
    public abstract TriggerHandler OnTriggered { get; } //다른 스킬에 의해 발동됐을 때


}
public struct SkillArgs
{
    public LivingEntity Caster;
    public Weapon Weapon;
    public Vector3 Origin;
    public Vector3 Direction;
}

public class SkillResult
{
    public List<AttackResult> AttackResults;
}

public struct AttackResult
{
    public LivingEntity Target;
    public int Damage;
    public Vector3 Origin;
}