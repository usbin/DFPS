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
                buff = new Buff_Haste(SkillImage);
                RemainCooltime = Cooltime;
                caster.Affect(buff);

            }
        }
        
        yield return null;
    }


    public class Buff_Haste : BaseBuff
    {
        public override string BuffName => "���̽�Ʈ";
        public override string BuffDescription => "���� �ð����� �̵��ӵ��� �� ��� �����մϴ�.";
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
