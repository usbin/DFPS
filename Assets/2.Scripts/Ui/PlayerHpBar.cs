using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerHpBar : MonoBehaviour
{
    public Player Player;
    Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (Player)
        {
            _slider.maxValue = Player.MaxHp;
            _slider.value = Player.Hp;
        }
    }
}
