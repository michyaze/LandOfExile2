using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapBackground : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        MenuControl.Instance.adventureMenu.ClickUpOnMap();

    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        MenuControl.Instance.adventureMenu.ClickDownOnMap();
    }
}
