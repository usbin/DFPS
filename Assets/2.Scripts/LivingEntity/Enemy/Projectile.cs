using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �������� ���ư���, �÷��̾�� �ε����� �������� �ְ� ������� �߻�ü
/// </summary>

public class Projectile : MonoBehaviour
{
    public ICombatable Attacker;
    public int Atk;
    public int Speed;
    public float Distance;
    public new SphereCollider collider;

    float _currentDistance; //���� ���ƿ� �Ÿ�

    Player _target;
    

    private void FixedUpdate()
    {
        //�ִ� �Ÿ����� ���ƿ����� �����
        if (_currentDistance > Distance)
        {
            Destroy(gameObject);
        }

        float movement = Speed * Time.deltaTime;//������ �Ÿ�
        Collider[] colliders = Physics.OverlapSphere(transform.position, collider.radius * transform.localScale.x, 1 << LayerMask.NameToLayer("Player"));
        
        if (colliders.Length>0)
        {
            _target = colliders[0].GetComponent<Player>();
            if(_target != null)
            {
                float damage = CombatSystem.CalculateInflictedDamage(Atk + Attacker.Atk, _target.Def);
                _target.TakeHit((int)damage);
                Destroy(gameObject);
            }
            else
            {
                //��� ���ư�.
                transform.Translate(Vector3.forward * movement);
                _currentDistance += movement;
            }
        }
        else
        {
            //��� ���ư�.
            transform.Translate(Vector3.forward * movement);
            _currentDistance += movement;

        }
    }

}
