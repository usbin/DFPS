using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : BaseItem
{
    BaseBuff _buff = new AppleBuff();
    

    public override BaseBuff Buff => _buff;


    // ���ݷ� 1 ����
    public class AppleBuff : BaseBuff
    {
        bool _done = false;

        int _atkEffect = 1;

        public override string BuffName => "���";
        public override string BuffDescription => "���";
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
