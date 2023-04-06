using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Show(string Text, Transform position) 하면 일정시간 후 알아서 사라짐.
/// </summary>

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class DamageEffect : MonoBehaviour
{
    TMPro.TextMeshProUGUI _text;
    float _alpha = 0f;
    bool _started = false;
    float _elapsedTime = 0; //이펙트를 시작하고 소요된 시간(x값)
    private void Awake()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void Show(string text, Vector3 position, Vector3 forward)
    {
        _text.text = text;
        _text.alpha = 1f;
        _alpha = _text.alpha;
        _started = true;
        transform.position = position;
        transform.forward = forward;
    }
    private void Update()
    {
        
        if (_started && _alpha <= 0f )
        {
            Destroy(gameObject);
            
        }
        else if(_alpha > 0f)
        {
            _elapsedTime += Time.deltaTime;
            _alpha -= 0.8f * Time.deltaTime;
            _text.transform.Translate(0f, Time.deltaTime*2f, 0f);
            _text.alpha = _alpha;
            _text.fontSize = Mathf.Max( -Mathf.Pow(40*_elapsedTime - 4f, 2) + 30, 25);
        }
        
    }
}
