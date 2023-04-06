using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Haste : BaseSkill
{

    protected override SetupHandler OnSetup => null;
    protected override SetdownHandler OnSetdown => null;
    protected override ExecuteHandler OnExecute => Execute;
    protected override TriggerHandler OnTriggered => null;

    const float s_duration = 3f;    //���ӽð�



    private void Update()
    {
        //ȭ�鿡 ǥ���ϱ� ���� ��Ÿ��
        if (RemainCooltime > 0)
        {
            RemainCooltime -= Time.deltaTime;
        }
    }

    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {
        if(RemainCooltime <= 0)
        {
            //�÷��̾� �̼��� 2��� ����
            LivingEntity caster = args.Caster;

            if (caster != null && !caster.Dead)
            {
                RemainCooltime = Cooltime;
                int originSpeed = caster.Speed;
                caster.Speed += originSpeed;
                float remainDuration = s_duration;
                while (remainDuration > 0)
                {
                    remainDuration -= Time.deltaTime;
                    yield return null;
                }
                caster.Speed -= originSpeed;

            }
        }
        
        yield return null;
    }
}
