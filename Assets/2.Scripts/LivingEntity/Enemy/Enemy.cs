using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public Projectile Projectile;


    const float cDistance = 5f;         // �ּ� �����Ÿ�
    const float cAttackDistance = 10f;   // ������ �����ϴ� �Ÿ�
    const float cAttackCooltime = 1f;

    Material _material;
    Color _defaultColor;
    NavMeshAgent _pathFinder;
    Transform _target;
    Stat _stat;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player) _target = player.transform;
        _pathFinder = GetComponent<NavMeshAgent>();

        _pathFinder.speed = Speed;
        _pathFinder.stoppingDistance = cDistance;
        _stat = Stat.Moving;
        _material = GetComponent<MeshRenderer>().material;
        _defaultColor = _material.color;
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
                Projectile projectile = Instantiate(Projectile, position, transform.rotation);
                projectile.Attacker = this;
                _stat = Stat.Moving;
                yield return new WaitForSeconds(cAttackCooltime);
            }
            else yield return null;

        }
    }
    public override void TakeHit(int damage, AttackArgs attackArgs)
    {
        StartCoroutine(HitEffect());
        base.TakeHit(damage, attackArgs);
    }
    IEnumerator HitEffect()
    {
        _material.color = Color.red;
        float duration = 0.01f;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
            
        }
        _material.color = _defaultColor;
    }

    enum Stat
    {
        Moving, Attacking
    }
}
