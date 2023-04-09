using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : BaseItem
{
    public override BaseBuff Buff => _buff;
    JuiceBuff _buff;
    private void Awake()
    {
        _buff = new JuiceBuff();
    }

    class JuiceBuff : BaseBuff
    {
        public override string BuffName => "주스";

        public override string BuffDescription => "이동속도 1 증가";

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
            owner.Speed += 1;
        }

        public override void UpdateBuff(Player owner)
        {
        }
    }
}
