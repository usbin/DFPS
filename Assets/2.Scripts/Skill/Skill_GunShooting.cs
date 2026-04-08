using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_GunShooting : BaseSkill
{
    public AudioClip ShootSound;
    public ParticleSystem AttackEffector;
    protected override SetupHandler OnSetup => null;

    protected override SetdownHandler OnSetdown => null;

    protected override ExecuteHandler OnExecute => Execute;

    protected override TriggerHandler OnTriggered => null;


    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {
        SkillResult result = new SkillResult();
        if (args.Weapon != null && args.Caster != null)
        {
            Gun gun = ((Gun)args.Weapon);
            if (gun != null)
            {
                SoundManager.Instance.PlaySound2(ShootSound, args.Caster.transform.position);
                Vector3 hitPoint = args.Origin + (args.Direction.normalized * (gun.Distance + args.Caster.Distance));
                Ray ray = new Ray(args.Origin, args.Direction);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, gun.Distance+args.Caster.Distance))
                {
                    LivingEntity combatable = hit.collider.GetComponent<LivingEntity>();
                    if (combatable != null && !combatable.Dead)
                    {
                        hitPoint = hit.point;
                        Debug.DrawRay(ray.origin, hitPoint - ray.origin);
                        //목적지에 이펙트 재생
                        if (AttackEffector != null)
                        {
                            AttackEffector.transform.position = hitPoint-args.Caster.transform.forward;
                            AttackEffector.Play();
                        }

                        //attacker의 스탯과 무기 damage를 계산
                        int totalDamage = CombatSystem.CalculateInflictedDamage(args.Caster.Atk + gun.Atk, combatable.Def);
                        AttackResult attackResult;
                        attackResult.Target = combatable;
                        attackResult.Damage = totalDamage;
                        attackResult.Origin = hitPoint;

                        //결과 저장
                        result.AttackResults = new List<AttackResult>();
                        result.AttackResults.Add(attackResult);



                        //공격 이벤트 발생
                        if (skillManager.OnAttack != null)
                            skillManager.OnAttack(args.Caster, combatable, totalDamage);
                        combatable.TakeHit(totalDamage);
                        if (gun.LineRenderer)
                        {
                            StartCoroutine(ShootLineEffect(gun.transform.position, hitPoint, gun.LineRenderer));
                        }
                        yield return result;
                    }

                    
                }
            }
        }
        
    }
    
    IEnumerator ShootLineEffect(Vector3 origin, Vector3 destination, LineRenderer lineRenderer)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, destination);
        float duration = 0.02f;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;

        }
        lineRenderer.enabled = false;
    }
}
