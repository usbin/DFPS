using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEffectController : MonoBehaviour
{
    Player _player;
    // ���� �÷��̾�� ����� ����Ʈ��
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
                // ����Ʈ ���� �Լ� ȣ��
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
        // ����Ʈ �����Լ� ȣ��
        effector.StartEffect(_player);
        _effects.Add(effector);
    }

}