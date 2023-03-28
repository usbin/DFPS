using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable, ICombatable
{
    public float MaxHp;
    public float Speed;
    protected float hp;
    protected bool dead = false;
    float _atk;
    float _def;
    public float Atk => _atk;
    public float Def => _def;

    public virtual void Start()
    {
        hp = MaxHp;
    }

    public virtual void Die()
    {
        dead = true;
        Destroy(gameObject);
    }

    public void TakeHit(float damage)
    {
        hp -= damage;
        if (hp <= 0 && !dead)
        {
            Die();
        }
    }
}
