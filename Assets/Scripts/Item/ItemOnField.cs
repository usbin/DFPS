using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어에게 닿으면 추가됨.
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

        //아직 주워지지 않았을 때만 충돌 검출
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
