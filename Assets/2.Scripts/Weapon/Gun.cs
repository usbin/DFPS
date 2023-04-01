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
    public override bool NormalAttack(AttackArgs args)
    {
        
        if (_remainShootTerm <= 0)
        {
            Ray ray = new Ray(args.Origin, args.Direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Distance))
            {
                ICombatable combatable = hit.collider.GetComponent<ICombatable>();
                if(combatable != null)
                {
                    Vector3 point = hit.point;
                    Debug.DrawRay(ray.origin, point - ray.origin);
                    //attacker�� ���Ȱ� ���� damage�� ���
                    float totalDamage = CombatSystem.CalculateInflictedDamage(args.Attacker.Atk + Atk, combatable.Def);
                    combatable.TakeHit((int)totalDamage, args);
                }
            }


            //������ �߻���/(1+AtkSpeed*0.1) = ���� �߻���
            _remainShootTerm = ShootTerm/(1f+args.Attacker.AtkSpeed*0.1f);
            return true;

        }
        else return false;
    }
}
