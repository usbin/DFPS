using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 방향으로 날아가며, 플레이어에게 부딪히면 데미지를 주고 사라지는 발사체
/// </summary>

public class Projectile : MonoBehaviour
{
    public ICombatable Attacker;
    public int Atk;
    public int Speed;
    public float Distance;
    public new SphereCollider collider;

    float _currentDistance; //현재 날아온 거리

    Player _target;
    

    private void FixedUpdate()
    {
        //최대 거리까지 날아왔으면 사라짐
        if (_currentDistance > Distance)
        {
            Destroy(gameObject);
        }

        float movement = Speed * Time.deltaTime;//움직일 거리
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
                //계속 날아감.
                transform.Translate(Vector3.forward * movement);
                _currentDistance += movement;
            }
        }
        else
        {
            //계속 날아감.
            transform.Translate(Vector3.forward * movement);
            _currentDistance += movement;

        }
    }

}
