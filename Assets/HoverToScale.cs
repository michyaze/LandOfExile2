using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverToScale : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    
    private float originalScale = 1f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * 1.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, 0.2f);
    }
}
