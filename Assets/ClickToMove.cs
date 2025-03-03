using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        return;
        // Check if imageToMove is assigned.
        if(MenuControl.Instance.adventureMenu. playerChess != null /*&& !MenuControl.Instance.adventureMenu.isMoving &&!MenuControl.Instance.adventureMenu.ignoreMapClicks*/ )
        {
            // Set the position of imageToMove to the position of the clicked image.
            //imageToMove.position = this.GetComponent<RectTransform>().position;
            //Camera.main.ScreenToViewportPoint(eventData.position);
            // var clickPosition = eventData.position;
            // var clickLocalPosition = clickPosition-  GetComponent<RectTransform>().anchoredPosition
            // ;
            RectTransform targetRectTransform = GetComponent<RectTransform>();

            // Convert screen position to local position within the target RectTransform
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetRectTransform, // Rectangle to get the local point within
                eventData.position,  // The screen position to convert
                eventData.pressEventCamera, // The camera used for the calculation (use null for Overlay Canvas)
                out Vector2 localCursor // This will be the output local position
            );
            StartCoroutine( MenuControl.Instance.adventureMenu. playerMove(localCursor, () =>
            {
                
                MenuControl.Instance.adventureMenu.currentMapTileIndex = -1;
            }));
        }
    }
}
