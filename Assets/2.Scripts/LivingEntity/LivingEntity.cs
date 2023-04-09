using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, ICombatable
{
    //�������
    public GameObject Graphic;
    public event System.Action<LivingEntity> OnDeath;
    public ParticleSystem DeathEffectorPrefab;
    public BaseItem[] DropItems;
    public int MaxHp;
    public int Hp { get => hp; }
    protected int hp;
    protected bool dead = false;
    public bool Dead { get => dead; }

    //���� ����
    public abstract BaseBuff[] AllActiveBuff { get; }
    public abstract void Affect(BaseBuff buff);

    //���� ����
    public int Speed;
    public int Distance;//�����Ÿ�
    [field: SerializeField]
    public int Atk { get; set; }
    [field: SerializeField]
    public int AtkSpeed { get; set; }
    [field: SerializeField]
    public int Def { get; set; }

    public GameObject GameObject
    {
        get
        {
            if (!dead) return gameObject;
            else return null;
        }
    }

    public virtual void Start()
    {
        hp = MaxHp;
    }
    public virtual void Update()
    {
        if (transform.position.y < -10) Die();
    }
    public abstract void ExecuteSkill(BaseSkill skill, SkillArgs args);

    public virtual void Die()
    {
        dead = true;
        Renderer renderer;
        if (TryGetComponent(out renderer)) renderer.enabled = false;
        if (Graphic != null) Graphic.SetActive(false);


        //������ ���
        if (DropItems.Length > 0)
        {
            //30% Ȯ���� ���
            if(Random.Range(1, 11) <=3)
            {
                int randomIndex = Random.Range(0, DropItems.Length);
                BaseItem item = Instantiate(DropItems[randomIndex]);
                item.transform.position = transform.position;
            }
        }

        //�̺�Ʈ �߻�
        if(OnDeath != null)
            OnDeath(this);

        //��� �ִϸ��̼�
        if (DeathEffectorPrefab != null)
        {
            ParticleSystem effector = Instantiate(DeathEffectorPrefab);
            effector.transform.position = transform.position;
            effector.Play();

            Destroy(gameObject);
        }
        else Destroy(gameObject);

        Debug.Log(gameObject.name + " ���");
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
