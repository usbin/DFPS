using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : BaseItem
{
    public override BaseBuff Buff => _buff;
    MilkBuff _buff;
    private void Awake()
    {
        _buff = new MilkBuff();
    }

    class MilkBuff : BaseBuff
    {
        public override string BuffName => "����";

        public override string BuffDescription => "���ݼӵ� 1 ����";

        public override Sprite BuffImage => null;

        public override float Duration => 0;

        public override float RemainDuration => Duration;

        public override void EndBuff(Player owner)
        {
        }

        public override bool IsEnd()
        {
            return true;
        }

        public override void StartBuff(Player owner)
        {
            owner.AtkSpeed += 1;
        }

        public override void UpdateBuff(Player owner)
        {
        }
    }
}
