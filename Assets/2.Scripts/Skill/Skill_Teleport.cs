using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Teleport : BaseSkill
{

    public override SetupHandler OnSetup => null;
    public override SetdownHandler OnSetdown => null;
    public override ExecuteHandler OnExecute => Execute;
    public override TriggerHandler OnTriggered => null;

    const float s_distance = 5f; //이동거리
    IEnumerator Execute(SkillArgs args, SkillManager skillManager)
    {
        //플레이어를 전방 3미터 앞으로 이동
        LivingEntity caster = args.Caster;
        Vector3 destination = caster.transform.position + caster.transform.forward * s_distance;
        Rigidbody casterRigidbody;

        if (caster != null && !caster.Dead && caster.TryGetComponent(out casterRigidbody))
        {
            Ray moveRay = new Ray(caster.transform.position, caster.transform.forward);
            RaycastHit hit;
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Item");
            //장애물이 있을 땐 y포지션만 0으로 수정해서 이동
            if (!Physics.Raycast(moveRay, out hit, s_distance, ~layerMask, QueryTriggerInteraction.Ignore))
            {
                casterRigidbody.MovePosition(destination);
            }

        }
        yield return null;
    }
}
