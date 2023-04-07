using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격을 광역공격으로 바꾸는 스킬
/// </summary>
public class Skill_MultipleShot : BaseSkill
{
    public BaseSkill[] TriggerList;//선행 스킬 목록
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
            //모든 타깃에 대해 전방 1m 광역 공격
            foreach (AttackResult attackResult in result.AttackResults)
            {
                //폭발 이펙트
                if (EffectorPrefab)
                {
                    ParticleSystem effector = Instantiate(EffectorPrefab);
                    effector.transform.position = attackResult.Origin;
                    effector.Play();
                }
                Collider[] colliders = Physics.OverlapSphere(attackResult.Origin, 2f, 1 << LayerMask.NameToLayer("Enemy"), QueryTriggerInteraction.Collide);
                for(int i=0; i < colliders.Length; i++)
                {
                    //실제 데미지 입히기
                    LivingEntity combatable;
                    if (colliders[i].TryGetComponent(out combatable) && !combatable.Dead)
                    {
                        int totalDamage = CombatSystem.CalculateInflictedDamage(attackResult.Damage, combatable.Def);

                        if (skillManager.OnAttack != null)
                            skillManager.OnAttack(args.Caster, combatable, totalDamage);

                        //광역공격의 중심이 된 오브젝트는 데미지 중복으로 주지 않도록 함.
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
