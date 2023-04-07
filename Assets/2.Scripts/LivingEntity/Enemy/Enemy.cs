using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : LivingEntity
{
    public event System.Action<Enemy, int> OnEnemyHit;

    public Transform DamageViewPoint;
    protected NavMeshAgent pathFinder;
    protected Transform target;
    public Stat CurrentStatus;

    public override BaseBuff[] AllActiveBuff => null;

    public virtual void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player) target = player.transform;
        pathFinder = GetComponent<NavMeshAgent>();
        pathFinder.speed = Speed;
    }
    public override void Start()
    {
        base.Start();
        StartCoroutine(RefreshPath());

    }

    public override void Update()
    {
        base.Update();
        if(Time.timeScale == 0)
        {
            pathFinder.isStopped = true;
        }
        else
        {
            pathFinder.isStopped = false;
        }
    }

    IEnumerator RefreshPath()
    {
        while(target && !dead)
        {
            //플레이어쪽으로 방향 틀기
            Vector3 eulerDifference = Quaternion.FromToRotation(transform.forward, target.transform.position - transform.position).eulerAngles;
            eulerDifference = new Vector3(eulerDifference.x % 360, eulerDifference.y % 360, eulerDifference.z % 360);


            if (CurrentStatus != Stat.Attacking)
            {
                pathFinder.SetDestination(target.position);

            }
            if ((eulerDifference.x > 60 && eulerDifference.x < 300)
            || (eulerDifference.y > 60 && eulerDifference.y < 300)
            || (eulerDifference.z > 60 && eulerDifference.z < 300))
            {
                float duration = 1f;
                while (duration > 0)
                {
                    Vector3 euler = new Vector3(eulerDifference.x > 180 ? eulerDifference.x - 360 : eulerDifference.x
                        , eulerDifference.y > 180 ? eulerDifference.y - 360 : eulerDifference.y
                        , eulerDifference.z > 180 ? eulerDifference.z - 360 : eulerDifference.z);
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + euler*Time.deltaTime);
                    duration -= Time.deltaTime;
                    yield return null;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    
    public override void TakeHit(int damage)
    {
        if(OnEnemyHit != null)
            OnEnemyHit(this, damage);
        base.TakeHit(damage);
    }


    public override void ExecuteSkill(BaseSkill skill, SkillArgs args)
    {
        //적 스킬은 아직 구현x
    }

    public override void Affect(BaseBuff buff)
    {
        // 적 버프는 아직 구현x
    }

    public enum Stat
    {
        Moving, Attacking
    }
}
