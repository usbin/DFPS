using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Presto : BaseSkill
{
    Buff_Presto _buff = new Buff_Presto();
    public override SetupHandler OnSetup => (LivingEntity owner, SkillManager skillManager) =>
    {
        //영구 버프 추가
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.Affect(_buff);
        }
    };
    public override SetdownHandler OnSetdown => (LivingEntity owner, SkillManager skillManager) =>
    {
        //영구 버프 삭제
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.RemoveBuffManual(_buff);
        }
    };
    public override ExecuteHandler OnExecute => null;
    public override TriggerHandler OnTriggered => null;

    class Buff_Presto : BaseBuff
    {
        public override void EndBuff(Player owner)
        {
            owner.AtkSpeed -= 100;
        }

        public override bool IsEnd()
        {
            return false;
        }

        public override void StartBuff(Player owner)
        {

            owner.AtkSpeed += 100;
        }

        public override void UpdateBuff(Player owner)
        {
        }
    }
}
