using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어에게 닿으면 추가됨.
/// </summary>
[RequireComponent(typeof(BaseItem))]
public class ItemOnField : MonoBehaviour
{
    BaseItem _item;
    bool _picked = false;
    PlayerBuffController _pickingPlayerEffectController;
    readonly float cGravitySqrt = Mathf.Sqrt(-1*Physics.gravity.y);

    private void Awake()
    {
        _item = GetComponent<BaseItem>();
    }
    private void Start()
    {
        //바닥으로 낙하
        StartCoroutine(FallDown());
    }
    void Update()
    {

        if(_item.transform.position.y < -10)
        {
            Destroy(gameObject);
        }


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
            PlayerBuffController controller = collision.gameObject.GetComponent<PlayerBuffController>();
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
            _pickingPlayerEffectController.Affect(_item.Buff);
            Destroy(gameObject);
        }
    }
}
