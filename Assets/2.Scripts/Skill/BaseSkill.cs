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
    //SkillResult ��ȯ

    protected abstract SetupHandler OnSetup { get; }       //����������
    protected abstract SetdownHandler OnSetdown { get; }   //���� ������� ��
    protected abstract ExecuteHandler OnExecute { get; }   //������� ��
    protected abstract TriggerHandler OnTriggered { get; } //�ٸ� ��ų�� ���� �ߵ����� ��


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