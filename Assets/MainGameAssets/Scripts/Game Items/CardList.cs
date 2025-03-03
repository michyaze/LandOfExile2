using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardList", menuName = "Game Data/Card Lists/CardList", order = 1)]
public class CardList : ScriptableObject
{
    public List<Card> cards = new List<Card>();
}
