using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable, ICombatable
{
    public event System.Action<LivingEntity> OnDeath;

    public Item[] DropItems;
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

        //아이템 드롭
        int randomIndex = Random.Range(0, DropItems.Length);
        Item item = Instantiate(DropItems[randomIndex]);
        item.transform.position = transform.position;

        //이벤트 발생
        OnDeath(this);

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
