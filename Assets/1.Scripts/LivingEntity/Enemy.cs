using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    // ������ �����ϴ� �Ÿ�
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
        //�÷��̾�� �ٰ����� ���� ��
    }

    enum Stat
    {
        Idle, Playing, Dead
    }
}
