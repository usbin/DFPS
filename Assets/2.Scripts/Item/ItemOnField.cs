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
    readonly float cGravitySqrt = Mathf.Sqrt(-1*Physics.gravity.y);

    private void Awake()
    {
        _item = GetComponent<Item>();
    }
    private void Start()
    {
        //바닥으로 낙하
        StartCoroutine(FallDown());
    }
    void Update()
    {
        
    }
    IEnumerator FallDown()
    {
        //아래쪽의 물체와의 거리가 0이 될 때까지
        float distance = cGravitySqrt * Time.deltaTime;
        while(!Physics.Raycast(_item.transform.position, Vector3.down, distance))
        {
            _item.transform.Translate(Vector3.down * distance);
            yield return null;
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!_picked && collision.gameObject.tag == "Player")
        {
            PlayerEffectController controller = collision.gameObject.GetComponent<PlayerEffectController>();
            if (controller)
            {
                _picked = true;
                _pickingPlayerEffectController = controller;
            }
        }
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
