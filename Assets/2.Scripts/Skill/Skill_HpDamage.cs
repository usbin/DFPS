using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HpDamage : BaseSkill
{
    BaseSkill.SkillManager.OnAttackHandler _onAttack = (LivingEntity attacker, LivingEntity target, int damage) =>
    {
        //체력 비례 데미지 추가
        target.TakeHit(attacker.MaxHp / 100);
    };


    public override SetupHandler OnSetup => (LivingEntity caster, SkillManager skillManager) =>
    {
        skillManager.OnAttack += _onAttack;
    };

    public override SetdownHandler OnSetdown => (LivingEntity caster, SkillManager skillManager) =>
    {
        skillManager.OnAttack -= _onAttack;
    };

    public override ExecuteHandler OnExecute => null;

    public override TriggerHandler OnTriggered => null;
}
