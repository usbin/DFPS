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
    public Skill_GunShooting ShootingPrefab;
    Skill_GunShooting _GunShooting;

    float _nextShootTime;
    CombatSystem _combatSystem;

    private void Awake()
    {
        _GunShooting = Instantiate(ShootingPrefab);
        _GunShooting.transform.SetParent(transform, false);
        _combatSystem = GameObject.FindGameObjectWithTag("CombatSystem").GetComponent<CombatSystem>();
    }
    public void Update()
    {
    }
    public override bool NormalAttack(LivingEntity owner, AttackArgs args)
    {
        
        if (_nextShootTime <= Time.time)
        {
            SkillArgs shootArgs;
            shootArgs.Caster = args.Attacker;
            shootArgs.Origin = args.Origin;
            shootArgs.Direction = args.Direction;
            shootArgs.Weapon = this;


            owner.ExecuteSkill(_GunShooting, shootArgs);

            //무기의 발사텀/(1+AtkSpeed*0.1) = 실제 발사텀
            _nextShootTime = Time.time + ShootTerm/(1f+args.Attacker.AtkSpeed*0.1f);
            return true;

        }
        else return false;
    }
    
}
