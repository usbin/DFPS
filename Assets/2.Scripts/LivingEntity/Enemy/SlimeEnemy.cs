using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SlimeEnemy : Enemy
{

    const float cDistance = 10f;         // �ּ� �����Ÿ�
    const float cAttackDistance = 10f;   // ������ �����ϴ� �Ÿ�
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
        //����
        //Ÿ�ٰ��� �Ÿ��� ����� ������
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
                //����
                //�÷��̾�� ����ü ������

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
    //�������� ���� Ÿ�ְ̹� �ִϸ��̼��� �ִ� ��ġ�� ���� ���� ���߱� ����
    
}
