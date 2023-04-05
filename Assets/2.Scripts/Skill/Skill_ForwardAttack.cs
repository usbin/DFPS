using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ForwardAttack : BaseSkill
{
    const float s_xDistance = 1f;       //전방 공격 폭
    const float s_zDistance = 3f;       //전방 공격 길이
    const float s_duration = 2f;        //지속시간
    const float s_damageTerm = 0.1f;    //데미지를 주는 텀
    const float s_cooltime = 2f;        //스킬 쿨타임
    float _nextCastTime;                //시전가능한 다음 시간
    public ParticleSystem EffectorPrefab;

    public override SetupHandler OnSetup => null;

    public override SetdownHandler OnSetdown => null;

    public override ExecuteHandler OnExecute => Execute;

    public override TriggerHandler OnTriggered => null;


    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {
        if (_nextCastTime <= Time.time)
        {
            //이펙트
            if (EffectorPrefab)
            {
                ParticleSystem effector = Instantiate(EffectorPrefab);
                effector.transform.position = Vector3.zero;
                effector.transform.SetParent(args.Caster.transform, false);
                effector.Play();
            }

            SkillResult result = new SkillResult();
            result.AttackResults = new List<AttackResult>();


            float remainDuration = s_duration;
            float remainTerm = s_damageTerm;

            LivingEntity caster = args.Caster;
            Transform casterTransform = caster.transform;
            int weaponAtk = 0;
            if (args.Weapon) weaponAtk = args.Weapon.Atk;
            while (caster != null && remainDuration > 0)
            {
                if (remainTerm < 0)
                {
                    Collider[] colliders = Physics.OverlapBox(
                     casterTransform.position + casterTransform.forward * s_zDistance / 2f
                   , new Vector3(s_xDistance / 2f, 1f, s_zDistance / 2f)
                   , Quaternion.LookRotation(casterTransform.forward)
                   , 1 << LayerMask.NameToLayer("Enemy"));
                    for(int i=0; i<colliders.Length; i++)
                    {
                        Enemy enemy = colliders[i].GetComponent<Enemy>();
                        int totalDamage = CombatSystem.CalculateInflictedDamage(caster.Atk+weaponAtk, enemy.Def);
                        //데미지 입히기
                        enemy.TakeHit(totalDamage);
                        //이벤트
                        if (skillManager.OnAttack != null)
                            skillManager.OnAttack(caster, enemy, totalDamage);
                        //결과 저장

                        AttackResult atkResult;
                        atkResult.Origin = enemy.transform.position;
                        atkResult.Target = enemy;
                        atkResult.Damage = totalDamage;
                        result.AttackResults.Add(atkResult);
                    }
                    remainTerm = s_damageTerm;
                    //쿨타임 세팅
                    _nextCastTime = Time.time + s_cooltime;


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
