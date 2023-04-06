using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AroundAttack : BaseSkill
{
    public ParticleSystem EffectorPrefab;
    protected override SetupHandler OnSetup => null;
    protected override SetdownHandler OnSetdown => null;
    protected override ExecuteHandler OnExecute => Execute;
    protected override TriggerHandler OnTriggered => null;

    const float s_duration = 2f;        //지속시간
    const float s_damageTerm = 0.1f;    //데미지 주는 텀


    
    private void Update()
    {
        //화면에 표시하기 위한 쿨타임
        if (RemainCooltime > 0)
        {
            RemainCooltime -= Time.deltaTime;
        }
    }

    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {

        //결과
        SkillResult result = new SkillResult();
        result.AttackResults = new List<AttackResult>();

        LivingEntity caster = args.Caster;

        if (RemainCooltime <= 0)
        {
            RemainCooltime = Cooltime;
            //이펙트
            if (EffectorPrefab)
            {
                ParticleSystem effector = Instantiate(EffectorPrefab);
                effector.transform.position = Vector3.zero;
                effector.transform.SetParent(caster.transform, false);
                effector.Play();
            }
            float remainDuration = s_duration;
            float remainTerm = s_damageTerm;
            while (remainDuration > 0)
            {
                if(remainTerm <= 0)
                {
                    //플레이어 주변을 공격
                    int weaponAtk = 0;
                    if (args.Weapon) weaponAtk = args.Weapon.Atk;
                    Vector3 casterPos = caster.transform.position;
                    Collider[] colliders = Physics.OverlapSphere(casterPos, 2f, 1 << LayerMask.NameToLayer("Enemy"));
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Enemy enemy = colliders[i].gameObject.GetComponent<Enemy>();
                        int totalDamage = CombatSystem.CalculateInflictedDamage(caster.Atk + weaponAtk, enemy.Def);
                        //실제 데미지
                        enemy.TakeHit(totalDamage);
                        //이벤트 실행
                        if(skillManager.OnAttack != null)
                            skillManager.OnAttack(caster, enemy, totalDamage);
                        //결과 저장
                        AttackResult atkResult;
                        atkResult.Origin = enemy.transform.position;
                        atkResult.Target = enemy;
                        atkResult.Damage = totalDamage;
                        result.AttackResults.Add(atkResult);
                    }

                    remainTerm = s_damageTerm;
                }
                remainDuration -= Time.deltaTime;
                remainTerm -= Time.deltaTime;
                yield return null;
            }
            yield return result;
        }
        yield return null;
    }
}
