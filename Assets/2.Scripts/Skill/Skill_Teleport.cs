using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Teleport : BaseSkill
{

    public override SetupHandler OnSetup => null;
    public override SetdownHandler OnSetdown => null;
    public override ExecuteHandler OnExecute => Execute;
    public override TriggerHandler OnTriggered => null;

    const float s_distance = 5f; //�̵��Ÿ�
    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {
        //�÷��̾ ���� 3���� ������ �̵�
        LivingEntity caster = args.Caster;
        Vector3 destination = caster.transform.position + caster.transform.forward * s_distance;
        Rigidbody casterRigidbody;

        if (caster != null && !caster.Dead && caster.TryGetComponent(out casterRigidbody))
        {
            Ray moveRay = new Ray(caster.transform.position, caster.transform.forward);
            RaycastHit hit;
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Item");
            //��ֹ��� ���� �� y�����Ǹ� 0���� �����ؼ� �̵�
            if (!Physics.Raycast(moveRay, out hit, s_distance, ~layerMask, QueryTriggerInteraction.Ignore))
            {
                casterRigidbody.MovePosition(destination);
            }

        }
        yield return null;
    }
}
