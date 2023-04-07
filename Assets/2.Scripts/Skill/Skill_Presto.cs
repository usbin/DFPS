using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Presto : BaseSkill
{
    Buff_Presto _buff;
    protected override SetupHandler OnSetup => (LivingEntity owner, SkillManager skillManager) =>
    {
        _buff = new Buff_Presto(SkillImage);
        //영구 버프 추가
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.Affect(_buff);
        }
    };
    protected override SetdownHandler OnSetdown => (LivingEntity owner, SkillManager skillManager) =>
    {
        //영구 버프 삭제
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.RemoveBuffManual(_buff);
        }
    };
    protected override ExecuteHandler OnExecute => null;
    protected override TriggerHandler OnTriggered => null;

    class Buff_Presto : BaseBuff
    {
        public override Sprite BuffImage => _buffImage;
        public override string BuffName => "프레스토";
        public override string BuffDescription => "공격속도가 대폭 상승합니다.";
        public override float Duration => -1;
        public override float RemainDuration => Duration;

        Sprite _buffImage;

        public Buff_Presto(Sprite image)
        {
            _buffImage = image;
        }

        public override void EndBuff(Player owner)
        {
            owner.AtkSpeed -= 500;
        }

        public override bool IsEnd()
        {
            return false;
        }

        public override void StartBuff(Player owner)
        {

            owner.AtkSpeed += 500;
        }

        public override void UpdateBuff(Player owner)
        {
        }
    }
}
