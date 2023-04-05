using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ������ �߰���.
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
        //�ٴ����� ����
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
