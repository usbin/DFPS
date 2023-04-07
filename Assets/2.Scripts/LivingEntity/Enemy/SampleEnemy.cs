using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : Enemy
{
    public Projectile Projectile;

    const float cDistance = 5f;         // 최소 유지거리
    const float cAttackDistance = 10f;   // 공격을 시작하는 거리
    const float cAttackCooltime = 1f;
    // Start is called before the first frame update


    public override void Awake()
    {
        base.Awake();
        pathFinder.stoppingDistance = cDistance;
        CurrentStatus = Stat.Moving;
    }
    public override void Start()
    {
        base.Start();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    IEnumerator Attack()
    {
        //공격
        //타겟과의 거리가 충분히 가까우면
        float cooltime = cAttackCooltime;
        while (target && !dead)
        {
            cooltime -= Time.deltaTime;
            if (cooltime <= 0)
            {
                float distanceFromTargetSqrt = (target.transform.position - transform.position).sqrMagnitude;
                float attackDistanceSqrt = Mathf.Pow(cAttackDistance, 2);
                if (distanceFromTargetSqrt < attackDistanceSqrt)
                {
                    CurrentStatus = Stat.Attacking;
                    //플레이어에게 투사체 날리기
                    Projectile projectile = Instantiate(Projectile, transform.position, Quaternion.LookRotation(target.position - transform.position));
                    projectile.Attacker = this;
                    CurrentStatus = Stat.Moving;
                    cooltime = cAttackCooltime;
                }
            }
            yield return null;



        }
    }
}
