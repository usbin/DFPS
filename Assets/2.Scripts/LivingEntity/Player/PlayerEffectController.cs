using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEffectController : MonoBehaviour
{
    Player _player;
    // 현재 플레이어에게 적용된 이펙트들
    List<BaseEffector> _effects = new List<BaseEffector>();
    List<BaseEffector> _endEffects;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void Update()
    {
        for(int i=_effects.Count-1; i>=0; i--)
        {
            if (_effects[i].IsEnd())
            {
                // 이펙트 종료 함수 호출
                _effects[i].EndEffect(_player);
                _effects.RemoveAt(i);
            }
            else
            {
                _effects[i].UpdateEffect(_player);
            }
        }
    }
    public void Affect(BaseEffector effector)
    {
        // 이펙트 시작함수 호출
        effector.StartEffect(_player);
        _effects.Add(effector);
    }

}
