using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ������������ �ٲٴ� ��ų
/// </summary>
public class Skill_MultipleShot : BaseSkill
{
    public BaseSkill[] TriggerList;//���� ��ų ���
    public ParticleSystem EffectorPrefab;

    protected override SetupHandler OnSetup => (LivingEntity owner, SkillManager skillExecutor) =>
    {
        foreach(BaseSkill skill in TriggerList)
        {
            skillExecutor.AddTriggeredSkill(skill.SkillName, this);
        }
    };

    protected override SetdownHandler OnSetdown => (LivingEntity owner, SkillManager skillExecutor) =>
    {
        skillExecutor.RemoveTriggeredSkill(this);
    };
    protected override ExecuteHandler OnExecute => null;

    protected override TriggerHandler OnTriggered => Execute;

    IEnumerator Execute(SkillArgs args, SkillResult result, SkillManager skillManager)
    {
        if (result != null && result.AttackResults != null)
        {
            //��� Ÿ�꿡 ���� ���� 1m ���� ����
            foreach (AttackResult attackResult in result.AttackResults)
            {
                //���� ����Ʈ
                if (EffectorPrefab)
                {
                    ParticleSystem effector = Instantiate(EffectorPrefab);
                    effector.transform.position = attackResult.Origin;
                    effector.Play();
                }
                Collider[] colliders = Physics.OverlapSphere(attackResult.Origin, 2f, 1 << LayerMask.NameToLayer("Enemy"), QueryTriggerInteraction.Collide);
                for(int i=0; i < colliders.Length; i++)
                {
                    //���� ������ ������
                    LivingEntity combatable;
                    if (colliders[i].TryGetComponent(out combatable) && !combatable.Dead)
                    {
                        int totalDamage = CombatSystem.CalculateInflictedDamage(attackResult.Damage, combatable.Def);

                        if (skillManager.OnAttack != null)
                            skillManager.OnAttack(args.Caster, combatable, totalDamage);

                        //���������� �߽��� �� ������Ʈ�� ������ �ߺ����� ���� �ʵ��� ��.
                        if(attackResult.Target == combatable)
                        {

                        }
                        else
                        {
                            combatable.TakeHit(totalDamage);
                        }
                        yield return result;
                    }
                }
            }
        }
    }


}
