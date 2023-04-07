using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Canvas TooltipCanvasPrefab;
    Canvas _tooltipCanvas;
    TMPro.TextMeshProUGUI _textUi;
    string _text;
    protected void Awake()
    {
        _tooltipCanvas = Instantiate(TooltipCanvasPrefab);
        _tooltipCanvas.gameObject.SetActive(false);
        _textUi = _tooltipCanvas.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        _textUi.alpha = 0f;
    }
    public void SetText(string text)
    {
        _text = text;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector2 mousePos = eventData.position;
        _textUi.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        _textUi.alpha = 1f;
        _textUi.text = _text;
        _tooltipCanvas.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textUi.alpha = 0f;
        _tooltipCanvas.gameObject.SetActive(false);
    }
}
