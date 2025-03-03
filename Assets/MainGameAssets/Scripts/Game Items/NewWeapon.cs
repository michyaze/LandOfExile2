using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeapon : Castable
{
    public int initialPower;
    public int initialDuality;
    private int duality;
    private int power;
    private bool hasInit;
    public bool exhaustAfterUsage;
    public Ability equipAbility;
    private Unit unit;
    public bool canTargetMinion;
    public bool canTargetHero = true;

    private void OnDestroy()
    {
        //Debug.Log("destroy weapon");
    }

    public void ChangePower(Ability ability, int newValue)
    {
        int oldValue = power;
        if (oldValue != newValue)
        {
            power = Mathf.Max(0, newValue);

            // foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            // {
            //     try
            //     {
            //         trigger.UnitChangedPower(this, ability, oldValue);
            //     }
            //     catch (System.Exception e)
            //     {
            //         Debug.LogError(e);
            //     }
            // }
        }
    }

    public void ChangeDuality(Ability ability,Unit unit, int newValue)
    {
        int oldValue = duality;
        if (oldValue != newValue)
        {
            duality = Mathf.Max(0, newValue);

            if (duality <= 0)
            {
                UnequipWeapon(unit, false);
            }

            // foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            // {
            //     try
            //     {
            //         trigger.UnitChangedPower(this, ability, oldValue);
            //     }
            //     catch (System.Exception e)
            //     {
            //         Debug.LogError(e);
            //     }
            // }
        }
    }
    
    public virtual int GetPower(Unit attacker = null, Unit defender = null)
    {
        if (!hasInit)
        {
            return initialPower;
        }
        //TODO modifiers for weapons

        return power;
    }

    public int GetDuality()
    { if (!hasInit)
        {
            return initialDuality;
        }
        return duality;
    }

    public override bool CanTarget(Tile tile)
    {
        Unit unit = tile.GetUnit();
        if (unit == null)
        {
            return false;
        }

        if ((unit is Hero && canTargetHero) || (unit is Minion && canTargetMinion))
        {
            if (unit.player == this.player)
            {
                return true;
            }
        }

        return false;
    }


    public override void TargetTile(Tile tile, bool payCost)
    {
        Unit unit = tile.GetUnit();
        if (payCost)
            player.PayCostFor(this);

        //If Casting from hand put into limbo
        if (player.cardsInHand.Contains(this))
        {
            player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.limbo);
        }

        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.CardPlayed(this, tile, null);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        }
        
        unit.ChangeWeapon(cardTemplate.GetComponent<NewWeapon>());
        
        
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.WeaponChangedTo(unit.weapon);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        }
    }

    public void UnequipWeapon(Unit hero, bool isReplace)
    {
        if (!isReplace)
        {
            if (hero == null)
            {
                Debug.LogError("hero is empty");
            }
            hero.ChangeWeapon(null);
        }
       // else
       if (exhaustAfterUsage)
       {
           ExhaustThisCard();
       }else
        {
            if (!player)
            {
                player = hero.player;
            }
            player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.discard);
        }
    }

    public void UseWeapon(Unit unit)
    {
        duality -= 1;
        if (duality <= 0)
        {
            UnequipWeapon(unit, false);
        }
    }

    public void InitWeapon(Unit unit)
    {
        duality = initialDuality;
        power = initialPower;
        hasInit = true;
        if (unit.GetTile())
        {
            equipAbility?.PerformAbility(this,unit.GetTile(),0);
        }
    }

    private void Start()
    {
        if (GetComponent<TriggerWeapon>() == null)
        {
            gameObject.AddComponent<TriggerWeapon>();
        }
    }
}
