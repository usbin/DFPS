using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HawkEye : BaseSkill
{
    Buff_HawkEye _buff;
    protected override SetupHandler OnSetup => (LivingEntity owner, SkillManager skillManager) =>
    {
        _buff = new Buff_HawkEye(SkillImage);
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


    public class Buff_HawkEye : BaseBuff
    {
        public override string BuffName => "호크아이";
        public override string BuffDescription => "사정거리가 대폭 증가합니다.";
        public override Sprite BuffImage => _buffImage;
        public override float Duration => -1f;//무한
        public override float RemainDuration => Duration;

        Sprite _buffImage;
        public Buff_HawkEye(Sprite image)
        {
            _buffImage = image;
        }

        public override void StartBuff(Player owner)
        {
            owner.Distance += 100;
        }

        public override void EndBuff(Player owner)
        {
            owner.Distance -= 100;
        }

        public override void UpdateBuff(Player owner)
        {
        }
        public override bool IsEnd()
        {
            return false;
        }
    }

}
