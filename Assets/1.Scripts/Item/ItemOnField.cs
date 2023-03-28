using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ������ �߰���.
/// </summary>
[RequireComponent(typeof(Item))]
public class ItemOnField : MonoBehaviour
{
    Item _item;
    bool _picked = false;
    PlayerEffectController _pickingPlayerEffectController;

    private void Awake()
    {
        _item = GetComponent<Item>();
    }
    void Start()
    {
        
    }

    void Update()
    {

        //���� �ֿ����� �ʾ��� ���� �浹 ����
        if (!_picked)
        {
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (!_picked && other.gameObject.tag == "Player")
        {
            PlayerEffectController controller = other.gameObject.GetComponent<PlayerEffectController>();
            if (controller)
            {
                _picked = true;
                _pickingPlayerEffectController = controller;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }


    private void FixedUpdate()
    {
        if (_picked)
        {
            _pickingPlayerEffectController.Affect(_item.ItemData.Effecter);
            Destroy(gameObject);
        }
    }
}
