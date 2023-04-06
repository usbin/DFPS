using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Haste : BaseSkill
{

    protected override SetupHandler OnSetup => null;
    protected override SetdownHandler OnSetdown => null;
    protected override ExecuteHandler OnExecute => Execute;
    protected override TriggerHandler OnTriggered => null;

    const float s_duration = 3f;    //지속시간



    private void Update()
    {
        //화면에 표시하기 위한 쿨타임
        if (RemainCooltime > 0)
        {
            RemainCooltime -= Time.deltaTime;
        }
    }

    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {
        if(RemainCooltime <= 0)
        {
            //플레이어 이속을 2배로 증가
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
