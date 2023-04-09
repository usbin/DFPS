using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SlimeEnemy : Enemy
{

    const float cDistance = 10f;         // 최소 유지거리
    const float cAttackDistance = 10f;   // 공격을 시작하는 거리
    const float cAttackCooltime = 2f;

    public Projectile Projectile;
    public Transform AttackBody;
    public Animator Animator;
    SphereCollider _collider;
    public override void Awake()
    {
        base.Awake();
        _collider = GetComponent<SphereCollider>();
        pathFinder.stoppingDistance = cDistance;
        CurrentStatus = Stat.Moving;
        Animator.SetBool("Moving", true);
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(Attack());
    }
    public override void Update()
    {
        base.Update();


    }
    IEnumerator Attack()
    {
        //공격
        //타겟과의 거리가 충분히 가까우면
        float cooltime = cAttackCooltime;
        while (target && !dead)
        {
            cooltime -= Time.deltaTime;
            Vector3 targetPos = target.transform.position;

            float distanceFromTargetSqrt = (target.transform.position - transform.position).sqrMagnitude;
            float attackDistanceSqrt = Mathf.Pow(cAttackDistance, 2);
            if (distanceFromTargetSqrt < attackDistanceSqrt
                && cooltime <= 0)
            {
                CurrentStatus = Stat.Attacking;
                Animator.SetBool("Moving", false);
                Animator.SetBool("Attack", true);
                //공격
                //플레이어에게 투사체 날리기

                cooltime = cAttackCooltime;
                float delay = 0.25f;
                while (delay > 0)
                {
                    delay -= Time.deltaTime;
                    cooltime -= Time.deltaTime;
                    yield return null;
                }
                if(target != null)
                {
                    Projectile projectile = Instantiate(Projectile, transform.position, Quaternion.LookRotation(targetPos - transform.position));
                    projectile.Attacker = this;
                }
               


            }
            else
            {
                CurrentStatus = Stat.Moving;
                Animator.SetBool("Moving", true);
                Animator.SetBool("Attack", false);
            }
            yield return null;
        }
    }
    //데미지가 들어가는 타이밍과 애니메이션이 최대 위치에 있을 때를 맞추기 위함
    
}
