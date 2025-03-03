using UnityEngine;

[CreateAssetMenu(fileName = "AnyCanTargetWithinHeroRange", menuName = "Game Data/Target Type/WithinRange", order = 1)]
public class AnyWithinHeroRange : TargetValidator
{
    public bool myTeam;
    public bool enemyTeam;
    public bool includeMinions = true;
    public bool includeHeroes = true;
    public bool nonLargeHeroes;
    public bool notMyHero;

    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {

        Unit otherUnit = tile.GetUnit();
        if (otherUnit != null)
        {
            if (!includeMinions && (otherUnit is Minion)) return false;
            if (!includeHeroes && (otherUnit is Hero)) return false;

            if (nonLargeHeroes && (otherUnit is LargeHero)) return false;
            if (notMyHero && otherUnit == unit.player.GetHero()) return false;

            if (enemyTeam && otherUnit.player == unit.player) return false;
            if (myTeam && otherUnit.player != unit.player) return false;
        }

        var range = 1;
        var weapon = unit.player.GetHero().weapon;
        if (weapon != null)
        {
            return weapon.activatedAbility.GetTargetValidator().CanUnitTargetTile(unit.player.GetHero(), tile);
        }
        else
        {
            return (unit.player.GetHero().GetAdjacentTiles(range).Contains(tile));
        }
    }
}