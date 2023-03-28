using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public Transform Muzzle;
    public float ShootTerm;
    public LayerMask Layermask;
    public float Distance;

    float _remainShootTerm;

    public void Update()
    {
        _remainShootTerm -= Time.deltaTime;
    }
    public override void NormalAttack(WeaponController.AttackArgs args)
    {
        if (_remainShootTerm <= 0)
        {
            Ray ray = new Ray(args.Origin, args.Direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Distance, Layermask, QueryTriggerInteraction.Collide))
            {
                Vector3 point = hit.point;
                Debug.DrawRay(ray.origin, point - ray.origin);
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                //attacker의 스탯과 무기 damage를 계산
                float totalDamage = CombatSystem.CalculateInflictedDamage(args.Attacker, this, damagable);
                if (damagable != null) damagable.TakeHit((int)totalDamage);
            }
            _remainShootTerm = ShootTerm;

        }
    }
}
