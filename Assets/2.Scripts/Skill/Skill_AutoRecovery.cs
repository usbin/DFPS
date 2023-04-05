using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AutoRecovery : BaseSkill
{
    Buff_AutoRecovery _buff = new Buff_AutoRecovery();

    public override SetupHandler OnSetup => (LivingEntity owner, SkillManager manager) =>
    {
        
        //�÷��̾� ������ �߰�
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.Affect(_buff);
        }
        
    };

    public override SetdownHandler OnSetdown => (LivingEntity owner, SkillManager manager) =>
    {
        //�÷��̾� �������� ����
        PlayerBuffController buffCtrl;
        if (owner.GameObject && owner.GameObject.TryGetComponent(out buffCtrl))
        {
            buffCtrl.RemoveBuffManual(_buff);
        }
    };

    public override ExecuteHandler OnExecute => null;

    public override TriggerHandler OnTriggered => null;




    public class Buff_AutoRecovery : BaseBuff
    {
        const float s_term = 5f;
        float remain = 0f;
        public override void StartBuff(Player owner)
        {
            remain = s_term;
        }
        public override void UpdateBuff(Player owner)
        {
            //1�ʸ��� 1% ȸ��
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
