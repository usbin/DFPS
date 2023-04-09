using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : BaseItem
{
    public override BaseBuff Buff => _buff;
    BreadBuff _buff;
    private void Awake()
    {
        _buff = new BreadBuff();
    }

    class BreadBuff : BaseBuff
    {
        public override string BuffName => "��";

        public override string BuffDescription => "�����Ÿ� 1 ����";

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
            owner.Distance += 1;
        }

        public override void UpdateBuff(Player owner)
        {
        }
    }
}
