using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HpDamage : BaseSkill
{
    BaseSkill.SkillManager.OnAttackHandler _onAttack = (LivingEntity attacker, LivingEntity target, int damage) =>
    {
        //ü�� ��� ������ �߰�
        target.TakeHit(attacker.MaxHp / 100);
    };


    protected override SetupHandler OnSetup => (LivingEntity caster, SkillManager skillManager) =>
    {
        skillManager.OnAttack += _onAttack;
    };

    protected override SetdownHandler OnSetdown => (LivingEntity caster, SkillManager skillManager) =>
    {
        skillManager.OnAttack -= _onAttack;
    };

    protected override ExecuteHandler OnExecute => null;

    protected override TriggerHandler OnTriggered => null;
}
