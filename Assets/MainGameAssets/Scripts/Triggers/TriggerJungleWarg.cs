using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJungleWarg : Trigger
{
    public int effectIndex = -1;
    public List<Effect> effectTemplates = new List<Effect>();
    public List<int> effectCharges = new List<int>();

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.UniqueID == "Green01" && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            int oldIndex = effectIndex;
            effectIndex += 1;
            if (effectIndex == effectTemplates.Count) effectIndex = 0;

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                Unit unit = ((Unit)GetCard());
                if (oldIndex >= 0)
                {
                    
                    foreach (Effect effect in unit.currentEffects.ToArray())
                    {
                        if (effect.originalTemplate == effectTemplates[oldIndex])
                        {
                            unit.RemoveEffect(GetCard(), this, effect);
                        }
                    }

                }
                unit.ApplyEffect(GetCard(), this, effectTemplates[effectIndex], effectCharges[effectIndex]);

            });
        }
    }
}
