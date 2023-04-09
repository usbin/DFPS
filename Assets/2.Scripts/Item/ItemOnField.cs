using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ������ �߰���.
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
        //�ٴ����� ����
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
        //�Ʒ����� ��ü���� �Ÿ��� 0�� �� ������
        float distance = _collider.bounds.center.y+_collider.bounds.size.y/2f+ cGravitySqrt * Time.deltaTime;
        while(!Physics.Raycast(_collider.bounds.center, Vector3.down, distance))
        {
            _item.transform.Translate(Vector3.down * distance);


            yield return null;
        }

        StartCoroutine(Floating());

    }
    //�սǵս�
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
