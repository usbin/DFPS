using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerBuffController : MonoBehaviour
{
    public BaseBuff[] AllActiveBuff => _buffes.ToArray();
    Player _player;
    // ���� �÷��̾�� ����� ����Ʈ��
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
                // ����Ʈ ���� �Լ� ȣ��
                _buffes[i].EndBuff(_player);
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
        // ����Ʈ �����Լ� ȣ��
        buff.StartBuff(_player);
        _buffes.Add(buff);
    }
    public void RemoveBuffManual(BaseBuff buff)
    {
        //�̺�Ʈ ���� ����
        buff.EndBuff(_player);
        _buffes.Remove(buff);
    }

}
