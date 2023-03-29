using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격력 1 증가
public class AppleEffector : BaseEffector
{
    bool _done = false;

    int _atkEffect = 1;
    public override void StartEffect(Player owner)
    {
        owner.Atk += _atkEffect;
        _done = true;
    }
    public override void UpdateEffect(Player owner)
    {
    }
    public override void EndEffect(Player owner)
    {
       
    }

    public override bool IsEnd()
    {
        return _done;   
    }
}
