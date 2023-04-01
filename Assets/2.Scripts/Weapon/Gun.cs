using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public Transform Muzzle;
    public float ShootTerm;
    public LayerMask Layermask;
    public float Distance;
    public LineRenderer LineRenderer;

    float _remainShootTerm;
    private void Awake()
    {
    }
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
                    //attacker의 스탯과 무기 damage를 계산
                    float totalDamage = CombatSystem.CalculateInflictedDamage(args.Attacker.Atk + Atk, combatable.Def);
                    combatable.TakeHit((int)totalDamage, args);
                    if (LineRenderer)
                    {
                        StartCoroutine(ShootLine(transform.position, point));
                    }
                }
            }


            //무기의 발사텀/(1+AtkSpeed*0.1) = 실제 발사텀
            _remainShootTerm = ShootTerm/(1f+args.Attacker.AtkSpeed*0.1f);
            return true;

        }
        else return false;
    }
    IEnumerator ShootLine(Vector3 origin, Vector3 destination)
    {
        LineRenderer.enabled = true;
        LineRenderer.SetPosition(0, origin);
        LineRenderer.SetPosition(1, destination);
        float duration = 0.02f;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;

        }
        LineRenderer.enabled = false;
    }
}
