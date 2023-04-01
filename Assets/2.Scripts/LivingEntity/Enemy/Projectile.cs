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
    

    float _currentDistance; //���� ���ƿ� �Ÿ�

    Player _target;
    

    private void FixedUpdate()
    {
        //�ִ� �Ÿ����� ���ƿ����� �����
        if (_currentDistance > Distance)
        {
            Destroy(gameObject);
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        float movement = Speed * Time.deltaTime;//������ �Ÿ�
        if (Physics.Raycast(ray, out hit, movement))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            _target = hit.collider.GetComponent<Player>();
            if(_target != null)
            {
                AttackArgs args;
                args.Attacker = Attacker;
                args.Defender = _target;
                args.Direction = ray.direction;
                args.Origin = ray.origin;
                float damage = CombatSystem.CalculateInflictedDamage(Atk + Attacker.Atk, _target.Def);
                _target.TakeHit((int)damage, args);
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
