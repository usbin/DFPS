using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HawkEye : BaseSkill
{
    Buff_HawkEye _buff = new Buff_HawkEye();
    public override SetupHandler OnSetup => (LivingEntity owner, SkillManager skillManager) =>
    {
        //���� ���� �߰�
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.Affect(_buff);
        }
    };

    public override SetdownHandler OnSetdown => (LivingEntity owner, SkillManager skillManager) =>
    {
        //���� ���� ����
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.RemoveBuffManual(_buff);
        }
    };

    public override ExecuteHandler OnExecute => null;

    public override TriggerHandler OnTriggered => null;


    public class Buff_HawkEye : BaseBuff
    {
        public override void StartBuff(Player owner)
        {
            owner.Distance += 10;
        }

        public override void EndBuff(Player owner)
        {
            owner.Distance -= 10;
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
