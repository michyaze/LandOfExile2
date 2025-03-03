using System;
using UnityEngine;

public class ActionAnimation : MonoBehaviour
{
    public Doozy.Engine.Soundy.SoundyData audioToPlay;
    public virtual float PerformAnimation(VisibleCard vc, Tile tile)
    {
        return 0f;
    }
}