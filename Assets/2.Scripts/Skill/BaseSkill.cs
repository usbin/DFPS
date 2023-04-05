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
    //SkillResult ��ȯ

    public abstract SetupHandler OnSetup { get; }       //����������
    public abstract SetdownHandler OnSetdown { get; }   //���� ������� ��
    public abstract ExecuteHandler OnExecute { get; }   //������� ��
    public abstract TriggerHandler OnTriggered { get; } //�ٸ� ��ų�� ���� �ߵ����� ��


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