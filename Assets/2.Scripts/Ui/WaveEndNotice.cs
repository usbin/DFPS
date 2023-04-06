using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class WaveEndNotice : MonoBehaviour
{
    TMPro.TextMeshProUGUI _text;
    float duration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            _text.alpha -= 0.5f*Time.deltaTime;

        }
        else Destroy(gameObject);
    }
}
