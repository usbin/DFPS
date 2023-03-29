using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public Projectile Projectile;

    const float cDistance = 5f;         // 최소 유지거리
    const float cAttackDistance = 10f;   // 공격을 시작하는 거리
    const float cAttackCooltime = 1f;

    NavMeshAgent _pathFinder;
    Transform _target;
    Stat _stat;
    private void Awake()
    {
        _pathFinder = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _pathFinder.speed = Speed;
        _pathFinder.stoppingDistance = cDistance;
        _stat = Stat.Moving;
    }
    public override void Start()
    {
        base.Start();
        StartCoroutine(RefreshPath());
        StartCoroutine(Attack());

    }

    // Update is called once per frame
    public override void Update()
    {

        base.Update();
    }

    IEnumerator RefreshPath()
    {
        while(_target && !dead && _stat!=Stat.Attacking)
        {
            _pathFinder.SetDestination(_target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Attack()
    {
        //공격
        //타겟과의 거리가 충분히 가까우면
        while (_target && !dead)
        {
            float distanceFromTargetSqrt = (_target.transform.position - transform.position).sqrMagnitude;
            float attackDistanceSqrt = Mathf.Pow(cAttackDistance, 2);
            if(distanceFromTargetSqrt < attackDistanceSqrt)
            {
                _stat = Stat.Attacking;
                //플레이어에게 투사체 날리기
                Vector3 position = new Vector3(transform.position.x, _target.position.y, transform.position.z);
                Instantiate(Projectile, position, transform.rotation);

                _stat = Stat.Moving;
                yield return new WaitForSeconds(cAttackCooltime);
            }
            else yield return null;

        }
    }


    enum Stat
    {
        Moving, Attacking
    }
}
