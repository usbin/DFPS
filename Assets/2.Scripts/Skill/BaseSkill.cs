using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract partial class BaseSkill : MonoBehaviour
{
    public string SkillName;
    public string SkillDescription;
    public Sprite SkillImage;
    public float Cooltime;
    public float RemainCooltime;


    public delegate void SetupHandler(LivingEntity owner, SkillManager skillExecutor);
    public delegate void SetdownHandler(LivingEntity owner, SkillManager skillExecutor);
    public delegate IEnumerator ExecuteHandler(SkillArgs args, SkillManager skillManager);
    public delegate IEnumerator TriggerHandler(SkillArgs args, SkillResult result, SkillManager skillManager);
    //SkillResult 반환

    protected abstract SetupHandler OnSetup { get; }       //습득했을때
    protected abstract SetdownHandler OnSetdown { get; }   //습득 취소했을 때
    protected abstract ExecuteHandler OnExecute { get; }   //사용했을 때
    protected abstract TriggerHandler OnTriggered { get; } //다른 스킬에 의해 발동됐을 때


}
public struct SkillArgs
{
    public LivingEntity Caster;
    public Weapon Weapon;   //nullable
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