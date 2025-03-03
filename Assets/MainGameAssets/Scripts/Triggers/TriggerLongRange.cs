using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class TriggerLongRange : Trigger
{
    public List<TargetValidator> normalRangeTargetValidators = new List<TargetValidator>();
    public List<TargetValidator> arcingTargetValidators = new List<TargetValidator>();

    public override void HeroSummoned(Hero newHero)
    {
        Hero hero = newHero;
        //I don't think this should ever called as weapon are not equipped out of battle
        NewWeapon deprecatedWeapon = newHero.weapon;
        if (hero == GetCard().player.GetHero() && deprecatedWeapon != null)
        {
            Debug.LogError("summon hero should not have weapon on it");
            TargetValidator original = deprecatedWeapon.activatedAbility.targetValidator;
            if (original != null)
            {
                int amountToAdjust = GetEffect().remainingCharges;
                int index = normalRangeTargetValidators.IndexOf(original);
                if (index > -1)
                {
                    int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, normalRangeTargetValidators.Count - 1);
                    deprecatedWeapon.activatedAbility.targetValidator = normalRangeTargetValidators[finalIndex];
                }
        
                index = arcingTargetValidators.IndexOf(original);
                if (index > -1)
                {
                    int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, arcingTargetValidators.Count - 1);
                    deprecatedWeapon.activatedAbility.targetValidator = arcingTargetValidators[finalIndex];
                }
            }
        }
    }

    public override void WeaponChanged(NewWeapon weaponTemplate, NewWeapon oldWeapon, Unit hero)
    {
        if (hero == GetCard().player.GetHero())
        {
            TargetValidator original = weaponTemplate.activatedAbility.targetValidator;
            if (original != null)
            {
                int amountToAdjust = GetEffect().remainingCharges;
                int index = normalRangeTargetValidators.IndexOf(original);
                if (index > -1)
                {
                    int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, normalRangeTargetValidators.Count - 1);
                    weaponTemplate.activatedAbility.targetValidator = normalRangeTargetValidators[finalIndex];
                }

                index = arcingTargetValidators.IndexOf(original);
                if (index > -1)
                {
                    int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, arcingTargetValidators.Count - 1);
                    weaponTemplate.activatedAbility.targetValidator = arcingTargetValidators[finalIndex];
                }
            }
        }
    }

    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (effect.UniqueID == "CommonEffect40")
        {
            List<Effect> rangeBlessings = GetCard().player.GetHero().GetEffectsByID("CommonEffect40");

            if (rangeBlessings.Count > 0)
            {
                int blessingCharges = 0;
                foreach (Effect blessing in rangeBlessings)
                {
                    blessingCharges += blessing.remainingCharges;
                }

                NewWeapon deprecatedWeapon = GetCard().player.GetHero().weapon;
                TargetValidator original = deprecatedWeapon.activatedAbility.targetValidator;
                if (original != null)
                {
                    int amountToAdjust = GetEffect().remainingCharges + blessingCharges;
                    int index = normalRangeTargetValidators.IndexOf(original);
                    if (index > -1)
                    {
                        int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, normalRangeTargetValidators.Count - 1);
                        deprecatedWeapon.activatedAbility.targetValidator = normalRangeTargetValidators[finalIndex];
                    }

                    index = arcingTargetValidators.IndexOf(original);
                    if (index > -1)
                    {
                        int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, arcingTargetValidators.Count - 1);
                        deprecatedWeapon.activatedAbility.targetValidator = arcingTargetValidators[finalIndex];
                    }
                }
            }
        }
    }

    public override void UnitRemovedEffect(Unit unit, Ability ability, Effect effect)
    {
        if (unit == GetCard().player.GetHero() && effect.UniqueID == "CommonEffect40")
        {
            NewWeapon deprecatedWeapon = GetCard().player.GetHero().weapon;
            TargetValidator original = deprecatedWeapon.activatedAbility.targetValidator;
            if (original != null)
            {
                int amountToAdjust = GetEffect().remainingCharges;
                int index = normalRangeTargetValidators.IndexOf(original);
                if (index > -1)
                {
                    int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, normalRangeTargetValidators.Count - 1);
                    deprecatedWeapon.activatedAbility.targetValidator = normalRangeTargetValidators[finalIndex];
                }

                index = arcingTargetValidators.IndexOf(original);
                if (index > -1)
                {
                    int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, arcingTargetValidators.Count - 1);
                    deprecatedWeapon.activatedAbility.targetValidator = arcingTargetValidators[finalIndex];
                }
            }
        }
    }

    public override void EffectChargesChanged(Ability ability, Effect effect, int previousCharges)
    {
        if (effect.UniqueID == "CommonEffect40")
        {
            List<Effect> rangeBlessings = GetCard().player.GetHero().GetEffectsByID("CommonEffect40");
            if (rangeBlessings.Count > 0)
            {
                int charges = 0;
                foreach (Effect blessing in rangeBlessings)
                {
                    charges += blessing.remainingCharges;
                }

                NewWeapon deprecatedWeapon = GetCard().player.GetHero().weapon;
                TargetValidator original = deprecatedWeapon.activatedAbility.targetValidator;
                if (original != null)
                {
                    int amountToAdjust = GetEffect().remainingCharges + charges;
                    int index = normalRangeTargetValidators.IndexOf(original);
                    if (index > -1)
                    {
                        int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, normalRangeTargetValidators.Count - 1);
                        deprecatedWeapon.activatedAbility.targetValidator = normalRangeTargetValidators[finalIndex];
                    }

                    index = arcingTargetValidators.IndexOf(original);
                    if (index > -1)
                    {
                        int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, arcingTargetValidators.Count - 1);
                        deprecatedWeapon.activatedAbility.targetValidator = arcingTargetValidators[finalIndex];
                    }
                }
            }
        }
    }
}
