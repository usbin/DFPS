using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Haste : BaseSkill
{

    protected override SetupHandler OnSetup => null;
    protected override SetdownHandler OnSetdown => null;
    protected override ExecuteHandler OnExecute => Execute;
    protected override TriggerHandler OnTriggered => null;

    Buff_Haste buff;

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
                buff = new Buff_Haste(SkillImage);
                RemainCooltime = Cooltime;
                caster.Affect(buff);

            }
        }
        
        yield return null;
    }


    public class Buff_Haste : BaseBuff
    {
        public override string BuffName => "헤이스트";
        public override string BuffDescription => "일정 시간동안 이동속도가 두 배로 증가합니다.";
        public override Sprite BuffImage => _buffImage;
        public override float Duration => 3f;
        public override float RemainDuration => _remainDuration;

        Sprite _buffImage;

        int _originSpeed;
        float _remainDuration;
        
        public Buff_Haste(Sprite image)
        {
            _buffImage = image;
            _remainDuration = Duration;
        }

        public override void StartBuff(Player owner)
        {
            _originSpeed = owner.Speed;
            owner.Speed += _originSpeed;
        }

        public override void EndBuff(Player owner)
        {
            owner.Speed -= _originSpeed;
        }

        public override void UpdateBuff(Player owner)
        {
            _remainDuration -= Time.deltaTime;
            
        }
        public override bool IsEnd()
        {
            return _remainDuration <= 0;
        }
    }
}
