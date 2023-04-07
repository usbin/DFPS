using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuffBar : MonoBehaviour
{
    public GameObject BuffSlotPrefab;
    public Transform Belt;
    public Player Player;
    BuffPull _pull;
    Dictionary<BaseBuff, GameObject> _activeBuffSlots = new Dictionary<BaseBuff, GameObject>();

    private void Awake()
    {
        _pull = new BuffPull(5, BuffSlotPrefab);
    }
    private void Start()
    {

        Player.OnBuffAdded += OnBuffAdded;
        Player.OnBuffRemoved += OnBuffRemoved;

    }
    private void Update()
    {
        //���� ����Ʈ�� �޾ƿͼ�
        //������ ���̰� ��.
        //�ٵ� �� ������ Instantiate�� �ϴ� �� ���� ���ϴϱ�
        //���� ������ �ϵ� ������ ���� ���� ��������.
        

        //���� ������Ʈ
        
    }

    void OnBuffAdded(BaseBuff buff)
    {
        GameObject slot = _pull.AllocBuffSlot();
        slot.transform.SetParent(Belt.transform, false);
        slot.GetComponent<Image>().sprite = buff.BuffImage;
        slot.GetComponent<ShowTooltip>().SetText(buff.BuffDescription);
        slot.transform.SetAsFirstSibling();
        _activeBuffSlots.Add(buff, slot);
    }

    void OnBuffRemoved(BaseBuff buff)
    {
        if (_activeBuffSlots.ContainsKey(buff))
        {
            _pull.DeallocBuffSlot(_activeBuffSlots[buff]);
            _activeBuffSlots.Remove(buff);
        }
    }

    private void OnDestroy()
    {

        Player.OnBuffAdded -= OnBuffAdded;
        Player.OnBuffRemoved -= OnBuffRemoved;
    }

    class BuffPull
    {
        GameObject _buffSlotPrefab;
        Stack<GameObject> _inactiveBuffSlots = new Stack<GameObject>();
        int _initCapacity;

        public BuffPull(int capacity, GameObject buffSlotPrefab)
        {
            _initCapacity = capacity;
            _buffSlotPrefab = buffSlotPrefab;
            IncreasePull(capacity);
        }

       public  GameObject AllocBuffSlot()
        {
            //��Ȱ��ȭ�� ������ ���� ��� Ǯ�� �ø���
            if (_inactiveBuffSlots.Count == 0)
            {
                IncreasePull(_initCapacity);
            }
            GameObject slot = _inactiveBuffSlots.Pop();
            slot.SetActive(true);
            return slot;
        }
        public void DeallocBuffSlot(GameObject buffSlot)
        {
            //���� �����
            buffSlot.SetActive(false);
            _inactiveBuffSlots.Push(buffSlot);
        }

        void IncreasePull(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                GameObject newSlot = Instantiate(_buffSlotPrefab);
                newSlot.SetActive(false);
                _inactiveBuffSlots.Push(newSlot);
            }
        }


    }

}
