using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHistoryMenu : Trigger
{

    public CardHistoryItem cardHistoryItemPrefab;
    List<CardHistoryItem> items = new List<CardHistoryItem>();

    public int maxItemsToShow = 5;

    public void ResetBattle()
    {
        foreach (CardHistoryItem item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (items.Count >= maxItemsToShow)
        {
            Destroy(items[0].gameObject);
            items.RemoveAt(0);
        }

        CardHistoryItem item = Instantiate(cardHistoryItemPrefab, transform);
        item.RenderCardHistoryItem(card, false, false);
        items.Add(item);
    }

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (items.Count >= maxItemsToShow)
        {
            Destroy(items[0].gameObject);
            items.RemoveAt(0);
        }

        CardHistoryItem item = Instantiate(cardHistoryItemPrefab, transform);
        item.RenderCardHistoryItem(card, true, false);
        items.Add(item);
    }

    public override void CardRemoved(Card card)
    {
        if (items.Count >= maxItemsToShow)
        {
            Destroy(items[0].gameObject);
            items.RemoveAt(0);
        }

        CardHistoryItem item = Instantiate(cardHistoryItemPrefab, transform);
        item.RenderCardHistoryItem(card, false, true);
        items.Add(item);
    }
}
