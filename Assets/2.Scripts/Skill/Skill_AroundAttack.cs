using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AroundAttack : BaseSkill
{
    public ParticleSystem EffectorPrefab;

    public override SetupHandler OnSetup => null;
    public override SetdownHandler OnSetdown => null;
    public override ExecuteHandler OnExecute => Execute;
    public override TriggerHandler OnTriggered => null;

    const float s_duration = 2f;        //���ӽð�
    const float s_damageTerm = 0.1f;    //������ �ִ� ��
    const float s_cooltime = 2f;        //��Ÿ��
    float _nextCastTime;                //���� ������ ���� �ð�

    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {

        //���
        SkillResult result = new SkillResult();
        result.AttackResults = new List<AttackResult>();

        LivingEntity caster = args.Caster;

        if (_nextCastTime <= Time.time)
        {
            //����Ʈ
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
                    //�÷��̾� �ֺ��� ����
                    int weaponAtk = 0;
                    if (args.Weapon) weaponAtk = args.Weapon.Atk;
                    Vector3 casterPos = caster.transform.position;
                    Collider[] colliders = Physics.OverlapSphere(casterPos, 2f, 1 << LayerMask.NameToLayer("Enemy"));
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Enemy enemy = colliders[i].gameObject.GetComponent<Enemy>();
                        int totalDamage = CombatSystem.CalculateInflictedDamage(caster.Atk + weaponAtk, enemy.Def);
                        //���� ������
                        enemy.TakeHit(totalDamage);
                        //�̺�Ʈ ����
                        if(skillManager.OnAttack != null)
                            skillManager.OnAttack(caster, enemy, totalDamage);
                        //��� ����
                        AttackResult atkResult;
                        atkResult.Origin = enemy.transform.position;
                        atkResult.Target = enemy;
                        atkResult.Damage = totalDamage;
                        result.AttackResults.Add(atkResult);
                    }


                    
                    remainTerm = s_damageTerm;
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
