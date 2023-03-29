using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    
    // 공격을 시작하는 거리
    const float cAttackDistance = 1f;
    NavMeshAgent _pathFinder;
    Transform _target;
    float _pathFindingCooltime;
    Stat _stat;
    private void Awake()
    {
        _pathFinder = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _pathFinder.speed = Speed;
        _pathFinder.stoppingDistance = cAttackDistance;
        _stat = Stat.Moving;
    }
    public override void Start()
    {
        base.Start();
        StartCoroutine(RefreshPath());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RefreshPath()
    {
        while(_target && !dead && _stat==Stat.Moving)
        {
            _pathFinder.SetDestination(_target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Attack()
    {
        //공격
        while (_target && !dead)
        {
            //플레이어에게 투사체 날리기
            yield return null;
        }
    }


    enum Stat
    {
        Moving, Attacking
    }
}
