using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEffectController : MonoBehaviour
{
    Player _player;
    // 현재 플레이어에게 적용된 이펙트들
    List<Effector> _effects = new List<Effector>();
    List<Effector> _endEffects;

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
                _effects[i].EndEffect(_player);
                _effects.RemoveAt(i);
            }
            else
            {
                _effects[i].UpdateEffect(_player);
            }
        }
    }
    public void Affect(Effector effector)
    {
        effector.StartEffect(_player);
        _effects.Add(effector);
    }

}
