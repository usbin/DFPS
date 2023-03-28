using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable, ICombatable
{
    public int MaxHp;
    public int Speed;
    protected int hp;
    protected bool dead = false;

    [field:SerializeField]
    public int Atk { get; set; }
    [field: SerializeField]
    public int Def { get; set; }

    public virtual void Start()
    {
        hp = MaxHp;
    }

    public virtual void Die()
    {
        dead = true;
        Destroy(gameObject);
    }

    public void TakeHit(int damage)
    {
        hp -= damage;
        if (hp <= 0 && !dead)
        {
            Die();
        }
    }
}
