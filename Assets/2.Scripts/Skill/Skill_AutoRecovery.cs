using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AutoRecovery : BaseSkill
{
    Buff_AutoRecovery _buff;

    protected override SetupHandler OnSetup => (LivingEntity owner, SkillManager manager) =>
    {
        _buff = new Buff_AutoRecovery(SkillImage);
        //플레이어 버프에 추가
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.Affect(_buff);
        }
        
    };

    protected override SetdownHandler OnSetdown => (LivingEntity owner, SkillManager manager) =>
    {
        //플레이어 버프에서 삭제
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.RemoveBuffManual(_buff);
        }
    };

    protected override ExecuteHandler OnExecute => null;

    protected override TriggerHandler OnTriggered => null;




    public class Buff_AutoRecovery : BaseBuff
    {

        public override Sprite BuffImage => _buffImage;
        Sprite _buffImage;
        public Buff_AutoRecovery(Sprite image)
        {
            _buffImage = image;
        }
        const float s_term = 5f;
        float remain = 0f;


        public override void StartBuff(Player owner)
        {
            remain = s_term;
        }
        public override void UpdateBuff(Player owner)
        {
            //1초마다 1% 회복
            remain -= Time.deltaTime;
            if (remain <= 0)
            {
                owner.Recover(owner.MaxHp / 100);
                remain = s_term;
            }

        }
        public override void EndBuff(Player owner)
        {

        }
        public override bool IsEnd()
        {
            return false;
        }
    }

}
