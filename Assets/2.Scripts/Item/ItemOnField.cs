using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어에게 닿으면 추가됨.
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(BaseItem))]
public class ItemOnField : MonoBehaviour
{
    BaseItem _item;
    bool _picked = false;
    PlayerBuffController _pickingPlayerEffectController;
    readonly float cGravitySqrt = Mathf.Sqrt(-1*Physics.gravity.y);
    Collider _collider;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _item = GetComponent<BaseItem>();
    }
    private void Start()
    {
        //바닥으로 낙하
        transform.Translate(Vector3.up * 1.5f);
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
        float distance = _collider.bounds.center.y+_collider.bounds.size.y/2f+ cGravitySqrt * Time.deltaTime;
        while(!Physics.Raycast(_collider.bounds.center, Vector3.down, distance))
        {
            _item.transform.Translate(Vector3.down * distance);


            yield return null;
        }

        StartCoroutine(Floating());

    }
    //둥실둥실
    IEnumerator Floating()
    {
        float maxY = transform.position.y + 2f;
        float minY = transform.position.y;
        while (gameObject != null)
        {
            while(transform.position.y > minY)
            {
                transform.Translate(Vector3.down * .5f * Time.deltaTime);
                yield return null;
            }
            while (transform.position.y < maxY)
            {
                transform.Translate(Vector3.up * .5f * Time.deltaTime);
                yield return null;
            }
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
