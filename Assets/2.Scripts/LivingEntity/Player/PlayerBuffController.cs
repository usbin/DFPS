using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerBuffController : MonoBehaviour
{
    public event System.Action<BaseBuff> OnBuffAdded;
    public event System.Action<BaseBuff> OnBuffRemoved;

    public BaseBuff[] AllActiveBuff => _buffes.ToArray();
    Player _player;
    // 현재 플레이어에게 적용된 이펙트들
    List<BaseBuff> _buffes = new List<BaseBuff>();

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void Update()
    {
        for(int i=_buffes.Count-1; i>=0; i--)
        {
            if (_buffes[i].IsEnd())
            {
                // 이펙트 종료 함수 호출
                _buffes[i].EndBuff(_player);
                if (OnBuffRemoved != null) OnBuffRemoved.Invoke(_buffes[i]);
                _buffes.RemoveAt(i);
            }
            else
            {
                _buffes[i].UpdateBuff(_player);
            }
        }
    }
    public void Affect(BaseBuff buff)
    {
        // 이펙트 시작함수 호출
        buff.StartBuff(_player);
        if(OnBuffAdded != null) OnBuffAdded(buff);
        _buffes.Add(buff);
    }
    public void RemoveBuffManual(BaseBuff buff)
    {
        //이벤트 수동 삭제
        buff.EndBuff(_player);
        if (OnBuffRemoved != null) OnBuffRemoved.Invoke(buff);
        _buffes.Remove(buff);
    }

}
