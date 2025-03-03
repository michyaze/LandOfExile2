using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerFlarePopupMenu : MonoBehaviour
{
    public List<VisibleCard> visibleCards;
    public List<Card> cards;

    private void Awake()
    {
        for (int i = 0; i < visibleCards.Count; i++)
        {
            visibleCards[i].RenderCard(cards[i]);
        }
    }
}
