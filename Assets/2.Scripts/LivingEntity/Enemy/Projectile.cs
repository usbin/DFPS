using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 방향으로 날아가며, 플레이어에게 부딪히면 데미지를 주고 사라지는 발사체
/// </summary>

public class Projectile : MonoBehaviour
{
    public int Atk;
    public int Speed;
    public float Distance;

    float _currentDistance; //현재 날아온 거리

    Player _target;
    

    private void FixedUpdate()
    {
        //최대 거리까지 날아왔으면 사라짐
        if (_currentDistance > Distance)
        {
            Destroy(gameObject);
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        float movement = Speed * Time.deltaTime;//움직일 거리
        if (Physics.Raycast(ray, out hit, movement))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            _target = hit.collider.GetComponent<Player>();
            if(_target != null)
            {
                CombatArgs args;
                args.AttackerAtk = Atk;
                args.DefenderDef = _target.Def;
                float damage = CombatSystem.CalculateInflictedDamage(args);
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
