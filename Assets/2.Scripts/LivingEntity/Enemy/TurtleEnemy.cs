using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TurtleEnemy : Enemy
{

    const float cDistance = 4f;         // �ּ� �����Ÿ�
    const float cAttackDistance = 4f;   // ������ �����ϴ� �Ÿ�
    const float cAttackCooltime = 2f;

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


            float distanceFromTargetSqrt = (target.transform.position - transform.position).sqrMagnitude;
            float attackDistanceSqrt = Mathf.Pow(cAttackDistance, 2);
            if (distanceFromTargetSqrt < attackDistanceSqrt
                &&cooltime<=0)
            {
                cooltime = cAttackCooltime;
                CurrentStatus = Stat.Attacking;
                Animator.SetBool("Moving", false);
                Animator.SetBool("Attack", true);
                //����(�������� ���� Ÿ�ְ̹� �ִϸ��̼� ���߱� ���� ������
                float delay = 0.7f;
                while (delay > 0)
                {
                    delay -= Time.deltaTime;
                    cooltime -= Time.deltaTime;
                    yield return null;
                }
                if (!dead && Physics.OverlapSphere(AttackBody.transform.position, _collider.radius * transform.localScale.x, 1 << LayerMask.NameToLayer("Player")).Length > 0)
                {
                    Player player = target.GetComponent<Player>();
                    player.TakeHit(CombatSystem.CalculateInflictedDamage(Atk, player.Def));

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
}
