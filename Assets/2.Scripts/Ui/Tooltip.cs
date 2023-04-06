using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TextMeshProUGUI TooltipObj;

    protected void Awake()
    {
        TooltipObj.alpha = 0f;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector2 mousePos = eventData.position;
        TooltipObj.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        TooltipObj.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipObj.alpha = 0f;
    }
}
