using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ForwardAttack : BaseSkill
{
    const float s_xDistance = 1f;       //���� ���� ��
    const float s_zDistance = 3f;       //���� ���� ����
    const float s_duration = 2f;        //���ӽð�
    const float s_damageTerm = 0.1f;    //�������� �ִ� ��
    const float s_cooltime = 2f;        //��ų ��Ÿ��
    float _nextCastTime;                //���������� ���� �ð�
    public ParticleSystem EffectorPrefab;

    public override SetupHandler OnSetup => null;

    public override SetdownHandler OnSetdown => null;

    public override ExecuteHandler OnExecute => Execute;

    public override TriggerHandler OnTriggered => null;


    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {
        if (_nextCastTime <= Time.time)
        {
            //����Ʈ
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
                        //������ ������
                        enemy.TakeHit(totalDamage);
                        //�̺�Ʈ
                        if (skillManager.OnAttack != null)
                            skillManager.OnAttack(caster, enemy, totalDamage);
                        //��� ����

                        AttackResult atkResult;
                        atkResult.Origin = enemy.transform.position;
                        atkResult.Target = enemy;
                        atkResult.Damage = totalDamage;
                        result.AttackResults.Add(atkResult);
                    }
                    remainTerm = s_damageTerm;
                    //��Ÿ�� ����
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
