using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, ICombatable
{
    public event System.Action<LivingEntity> OnDeath;
    public ParticleSystem DeathEffector;

    public BaseItem[] DropItems;
    public int MaxHp;
    public int Speed;
    public int Distance;//사정거리
    public int Hp { get => hp;}
    protected int hp;
    protected bool dead = false;
    public bool Dead { get => dead; }

    public GameObject GameObject
    {
        get
        {
            if (!dead) return gameObject;
            else return null;
        }
    }
    [field:SerializeField]
    public int Atk { get; set; }
    [field: SerializeField]
    public int AtkSpeed { get; set; }
    [field: SerializeField]
    public int Def { get; set; }

    public virtual void Start()
    {
        hp = MaxHp;
    }
    public virtual void Update()
    {
        if (transform.position.y < -10) Die();
    }
    public virtual void Die()
    {
        dead = true;
        GetComponent<Renderer>().enabled = false;


        //아이템 드롭
        if (DropItems.Length > 0)
        {
            int randomIndex = Random.Range(0, DropItems.Length);
            BaseItem item = Instantiate(DropItems[randomIndex]);
            item.transform.position = transform.position;
        }

        //이벤트 발생
        if(OnDeath != null)
            OnDeath(this);

        //사망 애니메이션
        if (DeathEffector != null)
        {
            
            StartCoroutine(DeathEffect(DeathEffector));
        }
        else Destroy(gameObject);

        Debug.Log(gameObject.name + " 사망");
    }

    IEnumerator DeathEffect(ParticleSystem effector)
    {

        effector.transform.position = transform.position;
        effector.Play();
        while (effector.IsAlive())
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    

    public virtual void TakeHit(int damage)
    {
        Mathf.Max(damage, 0);
        hp -= damage;
        
        if (hp <= 0 && !dead)
        {
            Die();
        }
    }
    public virtual void Recover(int amount)
    {
        Mathf.Max(amount, 0);
        hp += amount;
        if (hp > MaxHp)
        {
            hp = MaxHp;
        }
    }

}
