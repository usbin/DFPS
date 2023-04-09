using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : BaseItem
{
    public override BaseBuff Buff => _buff;
    MeatBuff _buff;
    private void Awake()
    {
        _buff = new MeatBuff();
    }

    class MeatBuff : BaseBuff
    {
        public override string BuffName => "���";

        public override string BuffDescription => "�ִ� ü�� 10 ����";

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
            owner.MaxHp += 10;
        }

        public override void UpdateBuff(Player owner)
        {
        }
    }
}
