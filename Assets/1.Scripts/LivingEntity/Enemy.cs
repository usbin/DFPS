using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    // 공격을 시작하는 거리
    float _attackDistance;

    public override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //플레이어에게 다가가며 총을 쏨
    }

    enum Stat
    {
        Idle, Playing, Dead
    }
}
