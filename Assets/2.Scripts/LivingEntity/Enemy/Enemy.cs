using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public Projectile Projectile;

    const float cDistance = 5f;         // �ּ� �����Ÿ�
    const float cAttackDistance = 10f;   // ������ �����ϴ� �Ÿ�
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
        //����
        //Ÿ�ٰ��� �Ÿ��� ����� ������
        while (_target && !dead)
        {
            float distanceFromTargetSqrt = (_target.transform.position - transform.position).sqrMagnitude;
            float attackDistanceSqrt = Mathf.Pow(cAttackDistance, 2);
            if(distanceFromTargetSqrt < attackDistanceSqrt)
            {
                _stat = Stat.Attacking;
                //�÷��̾�� ����ü ������
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
