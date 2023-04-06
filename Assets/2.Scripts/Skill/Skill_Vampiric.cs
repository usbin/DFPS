using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Vampiric : BaseSkill
{
    SkillManager.OnAttackHandler _onAttack = (LivingEntity attacker, LivingEntity target, int damage) =>
    {
        attacker.Recover(damage/10); // µ¥¹ÌÁöÀÇ 10%¸¸Å­ ÈíÇ÷
    };


    protected override SetupHandler OnSetup => (LivingEntity owner, SkillManager skillManager) =>
    {
        skillManager.OnAttack += _onAttack;
    };

    protected override SetdownHandler OnSetdown => (LivingEntity owner, SkillManager skillManager) =>
    {
        skillManager.OnAttack -= _onAttack;
    };

    protected override ExecuteHandler OnExecute => throw new System.NotImplementedException();

    protected override TriggerHandler OnTriggered => throw new System.NotImplementedException();
}
