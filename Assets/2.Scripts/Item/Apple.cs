using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : BaseItem
{
    BaseBuff _buff = new AppleBuff();
    

    public override BaseBuff Buff => _buff;


    // 공격력 1 증가
    public class AppleBuff : BaseBuff
    {
        bool _done = false;

        int _atkEffect = 1;

        public override string BuffName => "사과";
        public override string BuffDescription => "사과";
        public override Sprite BuffImage => null;
        public override float Duration => 0;
        public override float RemainDuration => 0;

        public override void StartBuff(Player owner)
        {
            owner.Atk += _atkEffect;
            _done = true;
        }
        public override void UpdateBuff(Player owner)
        {
        }
        public override void EndBuff(Player owner)
        {

        }

        public override bool IsEnd()
        {
            return _done;
        }
    }

}
