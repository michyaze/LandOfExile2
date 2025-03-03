using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Doozy.Engine.Soundy;
using System;
using UnityEditor;
using UnityEngine.Serialization;

public class BoardMenu : Trigger
{
    public CameraShake cameraShake;
    public GameObject damageIndicator;
    public GameObject healIndicator;
    public GameObject manaIndicator;
    public GameObject deckIndicator;
    public GameObject chargesIndicator;

    public GameObject attackIndicator;
    public GameObject buffIndicator;
    public GameObject moveIndicator;
    public GameObject tauntIndicator;

    public GameObject destroyVFXPrefab;
    public GameObject summoningVFXPrefab;
    public GameObject seasonsNiceSummoningVFXPrefab;
    public GameObject seasonsNaughtySummoningVFXPrefab;
    public GameObject boardCardAbilityFX;

    public List<Tile> tiles = new List<Tile>();
    public List<Unit> units = new List<Unit>();
    public List<Obstacle> obstacles = new List<Obstacle>();
    public List<WeatherTrap> traps = new List<WeatherTrap>();

    public int totalActiveTiles = 16;
    public int razedCards;
    public int addedCards;

    public SoundyData applyEffectSound;
    public SoundyData damageSound;
    public SoundyData summonSound;
    public SoundyData destroySound;
    public SoundyData healingSound;
    public SoundyData discardSound;
    public SoundyData deckShuffledSound;
    public SoundyData changePositiveSound;
    public SoundyData changeNegativeSound;

    public class QueuedIndicator
    {
        public Unit unit;
        public int typeInt;
    }
    public List<QueuedIndicator> queuedIndicators = new List<QueuedIndicator>();

    public int cameraShakeLevelWhenBigMinionSummoned = 15;

    void Update()
    {
        queuedIndicators.Clear();
    }

    int QueueIndicatorOnTile(Unit unit, int typeInt)
    {
        int count = 0;
        foreach (QueuedIndicator indicator2 in queuedIndicators)
        {
            if (indicator2.unit == unit && typeInt == indicator2.typeInt)
            {
                count += 1;
            }
        }

        QueuedIndicator indicator = new QueuedIndicator();
        indicator.unit = unit;
        indicator.typeInt = typeInt;
        queuedIndicators.Add(indicator);

        return count;
    }

    void Awake()
    {

        SetupBoard();

    }

    public void SetupBoard()
    {
        foreach (var ob in obstacles)
        {
            if (ob)
            {
                
                Destroy(ob.gameObject);
            }
        }
        foreach (var ob in traps)
        {
            if (ob)
            {
                Destroy(ob.gameObject);
            }
        }
        foreach (var ob in tiles)
        {
            if (ob)
            {
                ob.RemoveAllEffects();
            }
        }
        units.Clear();
        tiles.Clear();
        obstacles.Clear();
        traps.Clear();
        for (int ii = 0; ii < totalActiveTiles; ii += 1)
        {
            units.Add(null);
            obstacles.Add(null);
            traps.Add(null);
        }

        for (int ii = 0; ii < GetComponentsInChildren<Tile>(true).Length; ii += 1)
        {
            if (ii < totalActiveTiles)
            {
                tiles.Add(transform.GetChild(ii).GetComponent<Tile>());
            }
            transform.GetChild(ii).gameObject.SetActive(ii < totalActiveTiles);
        }
    }

    public void AddObstacleToTile(Obstacle obstacle, Tile tile)
    {
        for (int ii = 0; ii < totalActiveTiles; ii += 1)
        {
            if (tiles[ii] == tile)
            {
                obstacles[ii] = obstacle;
            }
        }
    }
    
    
    public void AddTrapToTile(WeatherTrap trap, Tile tile)
    {
        //if tile has unit, trigger move into
        for (int ii = 0; ii < totalActiveTiles; ii += 1)
        {
            if (tiles[ii] == tile)
            {
                traps[ii] = trap;
                break;
            }
        }
        
        var unit = tile.GetUnit();
        if (unit != null)
        {
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    //todo 这个处理有些问题，有的怪是进入格子时放trap，可能造成死循环，考虑单独做一个trigger
                    trigger.UnitMoved(unit, null, tile);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            
            
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.TrapGenerated(tile,trap);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        
        
    }

    public void RemoveTrap(WeatherTrap trap)
    {
        
        var tile = trap.GetTile();
        var unit = tile.GetUnit();
        if (unit != null)
        {
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitMoved(unit, tile, null);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        
        for (int ii = 0; ii < totalActiveTiles; ii += 1)
        {
            if (tiles[ii] == trap.GetTile())
            {
                traps[ii] = null;
                break;
            }
        }
        //Destroy(trap.gameObject);
        
    }
    public void MoveTrapToTile(WeatherTrap trap, Tile tile)
    {
        RemoveTrap(trap);

        AddTrapToTile(trap, tile);
        
        trap.transform.position = tile.transform.position;
    }
    public void MoveUnitToTile(Unit unit, Tile tile)
    {

        for (int ii = 0; ii < totalActiveTiles; ii += 1)
        {
            if (units[ii] == unit)
            {
                units[ii] = null;
            }
            if (tiles[ii] == tile)
            {
                units[ii] = unit;
            }
        }

        if (unit is LargeHero && tile != null)
        {
            for (int ii = 0; ii < totalActiveTiles; ii += 1)
            {
                if (tiles[ii] == tile.GetTileDown() || tiles[ii] == tile.GetTileRight() || tiles[ii] == tile.GetTileDown().GetTileRight())
                {
                    units[ii] = unit;
                }
            }
        }

    }

    public void RemoveUnitFromBoard(Unit unit)
    {
        MoveUnitToTile(unit, null);
    }

    public Tile GetTileOfUnit(Unit unit)
    {
        if (units.Contains(unit))
            return tiles[units.IndexOf(unit)];

        return null;
    }
    
    public Tile GetTileOfUnit(WeatherTrap unit)
    {
        if (traps.Contains(unit))
            return tiles[traps.IndexOf(unit)];

        return null;
    }


    public List<Tile> GetTilesOfLargeHero(LargeHero largeHero)
    {
        List<Tile> tilesToReturn = new List<Tile>();
        for (int ii = 0; ii < units.Count; ii += 1)
        {
            if (units[ii] == largeHero)
                tilesToReturn.Add(tiles[ii]);
        }
        return tilesToReturn;
    }

    public Unit GetUnitOnTile(Tile tile)
    {
        return units[tiles.IndexOf(tile)];
    }
    
    public Obstacle GetObstacleOnTile(Tile tile)
    {
        return obstacles[tiles.IndexOf(tile)];
    }
    public WeatherTrap GetTrapOnTile(Tile tile)
    {
        return traps[tiles.IndexOf(tile)];
    }

//check taunt
    public int tauntEnemyCount = 0;
    public void RenderBoard()
    {
        tauntEnemyCount = 0;
        foreach (var unit in units)
        {
            if (unit && unit.player == MenuControl.Instance.battleMenu.playerAI)
            {
                if (unit.GetEffectsOfType<TauntEffect>().Count != 0)
                {
                    tauntEnemyCount += 1;
                }
            }
        }
        foreach (Tile tile in tiles)
        {
            tile.RenderTile();
        }

    }

    bool BoxContains(Vector3 pointOnScreen, Vector3 screenMin, Vector3 screenMax)
    {
        return pointOnScreen.x <= screenMax.x &&
               pointOnScreen.y <= screenMax.y &&
               pointOnScreen.x >= screenMin.x &&
               pointOnScreen.y >= screenMin.y;
    }

    public Tile GetTileAtScreenPos(Vector3 mousePosition)
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);

        foreach (Tile tile in tiles)
        {
            RectTransform rectTransform = tile.GetComponent<RectTransform>();

            Vector3[] v = new Vector3[4];
            rectTransform.GetWorldCorners(v);
            float dist = Mathf.Abs(v[2].x - v[0].x) / 2f;

            if (BoxContains(mousePos, v[0], v[2]))
            {
                // Hit the rect transform
                return tile;
            }

        }

        return null;
    }
    public override void WeaponChanged(NewWeapon weaponTemplate, NewWeapon oldWeapon, Unit hero)
    {
        //1. create a visible card.
        //move it to discard
        //destroy it
        
        // NewWeapon weapon = Instantiate(weaponTemplate, transform);
        // newWeapon.transform.position = 
        // VisibleCard vc = oldWeapon.player.GetVisibleBoardCardForCard(oldWeapon);
        // if (vc != null)
        // {
        //     Vector3 pos = vc.card.player.discardPileText.transform.position;
        //     if (vc.card.GetZone() == MenuControl.Instance.battleMenu.removedFromGame)
        //     {
        //         pos = MenuControl.Instance.battleMenu.cardHistoryMenu.transform.position;
        //     }
        //
        //     GameObject vCToDiscard = Instantiate(vc.gameObject, vc.transform.parent) as GameObject;
        //     vCToDiscard.transform.position = vc.transform.position;
        //     vCToDiscard.transform.localScale = vc.transform.localScale;
        //     LeanTween.move(vCToDiscard, pos, MenuControl.Instance.battleMenu.GetPlaySpeed() * 1.5f).setEaseOutSine().setDestroyOnComplete(true);
        //     LeanTween.scale(vCToDiscard, Vector3.one * 0.5f, MenuControl.Instance.battleMenu.GetPlaySpeed());
        //     LeanTween.rotateAround(vCToDiscard, Vector3.forward, 720f, MenuControl.Instance.battleMenu.GetPlaySpeed() * 1.5f);
        //
        //     GameObject vfx = Instantiate(destroyVFXPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
        //     vfx.transform.position = vc.transform.position;
        //     Destroy(vfx, 5f);
        //
        //     //SoundyManager.Play(minion.deathSound);
        //
        // }
    }
    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        MenuControl.Instance.battleMenu.AddMinionDestroyed(minion);
        VisibleCard vc = minion.player.GetVisibleBoardCardForCard(minion);

        if (vc != null)
        {
            Vector3 pos = vc.card.player.discardPileText.transform.position;
            if (vc.card.GetZone() == MenuControl.Instance.battleMenu.removedFromGame)
            {
                pos = MenuControl.Instance.battleMenu.cardHistoryMenu.transform.position;
            }

            GameObject vCToDiscard = Instantiate(vc.gameObject, vc.transform.parent) as GameObject;
            vCToDiscard.transform.position = vc.transform.position;
            vCToDiscard.transform.localScale = vc.transform.localScale;
            LeanTween.move(vCToDiscard, pos, MenuControl.Instance.battleMenu.GetPlaySpeed() * 1.5f).setEaseOutSine().setDestroyOnComplete(true);
            LeanTween.scale(vCToDiscard, Vector3.one * 0.5f, MenuControl.Instance.battleMenu.GetPlaySpeed());
            LeanTween.rotateAround(vCToDiscard, Vector3.forward, 720f, MenuControl.Instance.battleMenu.GetPlaySpeed() * 1.5f);

            GameObject vfx = Instantiate(destroyVFXPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
            vfx.transform.position = vc.transform.position;
            Destroy(vfx, 5f);

            SoundyManager.Play(minion.deathSound);

        }

        if (ability != null && ability.GetCard() != null && ability.GetCard() is Unit && ability.GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            VisibleCard vcAffector = ability.GetCard().player.GetVisibleBoardCardForCard(ability.GetCard());
            if (vcAffector != null && Vector2.Distance(vcAffector.transform.position, ((Unit)ability.GetCard()).GetTile().transform.position) < 0.1f)
            {
                GameObject boardAbilityVFX = Instantiate(boardCardAbilityFX, MenuControl.Instance.battleMenu.transform) as GameObject;

                boardAbilityVFX.transform.position = vcAffector.transform.position;
                Destroy(boardAbilityVFX, 3f);
            }
        }

        SoundyManager.Play(destroySound);

    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        MenuControl.Instance.battleMenu.AddUsedCardUniqueId(card.UniqueID);
    }

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit.player != null && unit.player.GetVisibleBoardCardForCard(unit))
        {
            float playSpeed = MenuControl.Instance.battleMenu.GetPlaySpeed();

            // create indicator
            int delayUnits = QueueIndicatorOnTile(unit, 0);
            if (delayUnits > 50) return;
            VisibleCard vc = unit.player.GetVisibleBoardCardForCard(unit);
            GameObject obj = Instantiate(damageIndicator, MenuControl.Instance.battleMenu.transform) as GameObject;
            obj.GetComponentInChildren<Text>().text = damageAmount.ToString();
            //obj.transform.position = vc.transform.position + vc.damagePoint + (Vector3.up * delayUnits * 0.3f);
            if (unit.GetTile())
            {
                obj.transform.position = unit.GetTile().transform.position + vc.damagePoint + (Vector3.up * delayUnits * 0.3f);
            }
            else
            {
                obj.transform.position = vc.transform.position + vc.damagePoint + (Vector3.up * delayUnits * 0.3f);
            }
            
            float timeToLive = playSpeed * 3f;
            Destroy(obj, timeToLive);

            LeanTween.move(obj, obj.transform.position + Vector3.up * 0.3f, timeToLive).setEaseOutSine();
            LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(), 0f, timeToLive / 2).setDelay(timeToLive / 2);
            LeanTween.scale(obj, Vector3.one * 1.3f, timeToLive).setEaseOutSine();
            SoundyManager.Play(damageSound);

            SoundyManager.Play(unit.damagedSound);

            cameraShake.ShakeFromHit(damageAmount);

            //Reaction if from unit on board

            if (ability != null && ability.GetCard() != null && ability.GetCard() is Unit)
            {
                Unit attacker = (Unit)ability.GetCard();
                VisibleCard vcAttacker = attacker.player.GetVisibleBoardCardForCard(attacker);
                if (attacker.GetZone() == MenuControl.Instance.battleMenu.board && vcAttacker != null)
                {

                    if (attacker != unit)
                    {
                        Vector3 origPos = vc.transform.position;
                        Vector2 vec1 = vc.GetComponent<RectTransform>().anchoredPosition;
                        Vector3 pos1 = vc.transform.position - ((vcAttacker.transform.position - vc.transform.position).normalized * 0.4f);
                        vc.transform.position = pos1;
                        Vector2 vec2 = vc.GetComponent<RectTransform>().anchoredPosition;
                        vc.transform.position = origPos;

                        LeanTween.move(vc.GetComponent<RectTransform>(), vec2, playSpeed * 0.15f).setEaseOutQuad();
                        LeanTween.move(vc.GetComponent<RectTransform>(), vec1, playSpeed * 0.3f).setDelay(playSpeed * 0.7f).setEaseInOutSine();
                        LeanTween.rotateAround(vc.gameObject, Vector3.forward, 8f, playSpeed * 0.15f);
                        LeanTween.rotate(vc.gameObject, Vector3.zero, playSpeed * 0.3f).setDelay(playSpeed * 0.7f).setEaseInOutSine();

                        if (Vector2.Distance(vcAttacker.transform.position, attacker.GetTile().transform.position) < 0.1f)
                        {
                            GameObject boardAbilityVFX = Instantiate(boardCardAbilityFX, MenuControl.Instance.battleMenu.transform) as GameObject;
                            boardAbilityVFX.transform.position = vcAttacker.transform.position;
                            Destroy(boardAbilityVFX, 3f);
                        }
                    }
                }

            }
            else
            {
                LeanTween.rotateAround(vc.gameObject, Vector3.forward, 8f, playSpeed / 6f).setEaseOutQuad().setLoopPingPong(2);
            }

            // if (ability != null && ability.GetCard() != null)
            //     Debug.Log(unit.UniqueID + " damaged by " + ability.GetCard().UniqueID + " for " + damageAmount);

        }


    }

    public override void UnitHealed(Unit unit, Ability ability, int healAmount)
    {
        // create indicator
        if (unit.player != null && unit.player.GetVisibleBoardCardForCard(unit))
        {
            GameObject obj = Instantiate(healIndicator, MenuControl.Instance.battleMenu.transform) as GameObject;
            obj.GetComponentInChildren<Text>().text = healAmount.ToString();
            obj.transform.position = unit.player.GetVisibleBoardCardForCard(unit).transform.position;

            Destroy(obj, MenuControl.Instance.battleMenu.GetPlaySpeed() * 2f);

            float timeToLive = MenuControl.Instance.battleMenu.GetPlaySpeed() * 3f;
            Destroy(obj, timeToLive);

            LeanTween.move(obj, obj.transform.position + Vector3.up * 0.3f, timeToLive).setEaseOutSine();
            LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(), 0f, timeToLive / 2).setDelay(timeToLive / 2);
            LeanTween.scale(obj, Vector3.one * 1.3f, timeToLive).setEaseOutSine();

            if (ability != null && ability.GetCard() != null && ability.GetCard() is Unit && ability.GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
            {
                GameObject boardAbilityVFX = Instantiate(boardCardAbilityFX, MenuControl.Instance.battleMenu.transform) as GameObject;
                VisibleCard vcAffector = ability.GetCard().player.GetVisibleBoardCardForCard(ability.GetCard());
                boardAbilityVFX.transform.position = vcAffector.transform.position;
                Destroy(boardAbilityVFX, 3f);
            }

            Debug.Log(unit.UniqueID + " healed by " + ability.GetCard().UniqueID + " for " + healAmount);

        }

        SoundyManager.Play(healingSound);
    }

    
    public override void CardRemoved(Card card)
    {

        if (card.player == MenuControl.Instance.battleMenu.player1)// && previousZone == MenuControl.Instance.battleMenu.deck)
        {
            float playSpeed = MenuControl.Instance.battleMenu.GetPlaySpeed();
            razedCards += 1;
            float razeDelayInterval = playSpeed * 2f;
            card.player.deckPileText.text = (card.player.cardsInDeck.Count + razedCards).ToString();

            LeanTween.delayedCall(razedCards * razeDelayInterval, () =>
            {
                razedCards -= 1;
                card.player.deckPileText.text = (card.player.cardsInDeck.Count + razedCards).ToString();

                VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, MenuControl.Instance.battleMenu.transform);
                vc.RenderCardForMenu(card);
                vc.disableInteraction = true;
                vc.transform.position = MenuControl.Instance.battleMenu.player1.deckPileText.transform.position;

                vc.transform.DOMove(Vector3.left * 4f + Vector3.up * 0.5f, playSpeed * 0.7f).SetEase(Ease.InOutCubic);
                vc.transform.DORotate(Vector3.zero, playSpeed * 0.7f).SetEase(Ease.InSine);
                vc.transform.DOScale(Vector3.one * 1.5f, playSpeed * 0.7f);

                LeanTween.delayedCall(playSpeed * 2f, () =>
                {
                    LeanTween.move(vc.gameObject, MenuControl.Instance.battleMenu.cardHistoryMenu.transform.position, playSpeed * 1.5f).setEaseOutSine().setDestroyOnComplete(true);
                    LeanTween.scale(vc.gameObject, Vector3.one * 0.5f, playSpeed);
                    LeanTween.rotateAround(vc.gameObject, Vector3.forward, 720f, playSpeed * 1.5f);

                });

            });
        }
    }

    
    public override void CardAddedIntoDeck(Card card)
    {
        if (card.player == MenuControl.Instance.battleMenu.player1)// && previousZone == MenuControl.Instance.battleMenu.deck)
        {
            float playSpeed = MenuControl.Instance.battleMenu.GetPlaySpeed();
            addedCards += 1;
            float razeDelayInterval = playSpeed * 2f;
            card.player.deckPileText.text = (card.player.cardsInDeck.Count - addedCards).ToString();

            LeanTween.delayedCall(addedCards * razeDelayInterval, () =>
            {
                addedCards -= 1;
                card.player.deckPileText.text = (card.player.cardsInDeck.Count - addedCards).ToString();

                VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, MenuControl.Instance.battleMenu.transform);
                vc.RenderCardForMenu(card);
                vc.disableInteraction = true;
                vc.transform.position = MenuControl.Instance.battleMenu.cardHistoryMenu.transform.position;

                vc.transform.DOMove(Vector3.left * 4f + Vector3.up * 0.5f, playSpeed * 0.7f).SetEase(Ease.InOutCubic);
                vc.transform.DORotate(Vector3.zero, playSpeed * 0.7f).SetEase(Ease.InSine);
                vc.transform.DOScale(Vector3.one * 1.5f, playSpeed * 0.7f);

                LeanTween.delayedCall(playSpeed * 2f, () =>
                {
                    LeanTween.move(vc.gameObject, MenuControl.Instance.battleMenu.player1.deckPileText.transform.position, playSpeed * 1.5f).setEaseOutSine().setDestroyOnComplete(true);
                    LeanTween.scale(vc.gameObject, Vector3.one * 0.5f, playSpeed);
                    LeanTween.rotateAround(vc.gameObject, Vector3.forward, 720f, playSpeed * 1.5f);

                });

            });
        }
    }

    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        SoundyManager.Play(applyEffectSound);
        VisibleCard vc = unit.player.GetVisibleBoardCardForCard(unit);
        if (vc != null)
        {
            int posIndex = 0;
            int negIndex = 0;
            var currentEffects = new List<Effect>( unit.currentEffects);
            currentEffects.Reverse();
            foreach (Effect otherEffect in currentEffects)
            {
                if (otherEffect.isPositive && posIndex < 3)
                {
                    if (otherEffect == effect)
                    {
                        int killedCount = vc.positiveEffectImages[posIndex].transform.parent.DOKill();
                        //Debug.Log(("killed count "+killedCount));
                        vc.positiveEffectImages[posIndex].transform.parent.localScale = Vector3.one;
                        
                        //LeanTween.scale(vc.positiveEffectImages[posIndex].transform.parent.gameObject, Vector3.one * 2f, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseOutSine().setLoopPingPong(1);
                        vc.positiveEffectImages[posIndex].transform.parent
                            .DOScale(Vector3.one * 2f, MenuControl.Instance.battleMenu.GetPlaySpeed())
                            .SetLoops(2, LoopType.Yoyo);
                    }
                    posIndex += 1;
                }

                if (!otherEffect.isPositive && negIndex < 3)
                {
                    if (otherEffect == effect)
                    {
                        vc.negativeEffectImages[negIndex].transform.parent.localScale = Vector3.one;
                        vc.negativeEffectImages[negIndex].transform.parent.DOKill();
                        //LeanTween.scale(vc.negativeEffectImages[posIndex].transform.parent.gameObject, Vector3.one * 2f, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseOutSine().setLoopPingPong(1);
                        vc.negativeEffectImages[negIndex].transform.parent
                            .DOScale(Vector3.one * 2f, MenuControl.Instance.battleMenu.GetPlaySpeed())
                            .SetLoops(2, LoopType.Yoyo);
                    }
                    negIndex += 1;
                }
            }

            if (ability != null && ability.GetCard() != null && ability.GetCard() is Unit && ability.GetCard().GetZone() == MenuControl.Instance.battleMenu.board && ability.GetCard() != unit)
            {
                GameObject boardAbilityVFX = Instantiate(boardCardAbilityFX, MenuControl.Instance.battleMenu.transform) as GameObject;
                VisibleCard vcAffector = ability.GetCard().player.GetVisibleBoardCardForCard(ability.GetCard());
                boardAbilityVFX.transform.position = vcAffector.transform.position;
                Destroy(boardAbilityVFX, 3f);
            }
        }

    }

    public override void EffectChargesChanged(Ability ability, Effect effect, int previousCharges)
    {
        // create indicator
        int deltaAmount = effect.remainingCharges - previousCharges;
        if (effect.GetCard() != null && effect.GetCard().player.GetVisibleBoardCardForCard(effect.GetCard()) != null)
        {
            VisibleCard vc = effect.GetCard().player.GetVisibleBoardCardForCard(effect.GetCard());
            vc.RenderCardOnBoard(vc.card);
            Vector3 pos = Vector3.zero;
            foreach (Image image in vc.positiveEffectImages)
            {
                if (image.sprite == effect.GetSprite())
                {
                    pos = image.transform.parent.localPosition;
                }
            }
            foreach (Image image in vc.negativeEffectImages)
            {
                if (image.sprite == effect.GetSprite())
                {
                    pos = image.transform.parent.localPosition;
                }
            }
            if (deltaAmount != 0 && pos != Vector3.zero)
            {
                GameObject obj = Instantiate(chargesIndicator, vc.transform) as GameObject;
                obj.GetComponentInChildren<Text>().text = effect.remainingCharges.ToString();
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one * 1.3f;

                Destroy(obj, MenuControl.Instance.battleMenu.GetPlaySpeed() * 2f);

                float timeToLive = MenuControl.Instance.battleMenu.GetPlaySpeed() * 3f;
                Destroy(obj, timeToLive);

                LeanTween.moveLocal(obj, pos, timeToLive).setEaseOutSine();
                LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(), 0f, timeToLive / 2).setDelay(timeToLive / 2);
                LeanTween.scale(obj, Vector3.one, timeToLive).setEaseOutSine();

            }

        }
    }

    public override void UnitFinishRemovedEffect(Unit unit, Ability ability, Effect effect)
    {
        
        if (effect.GetCard() != null && effect.GetCard().player.GetVisibleBoardCardForCard(effect.GetCard()) != null)
        {
            VisibleCard vc = effect.GetCard().player.GetVisibleBoardCardForCard(effect.GetCard());
            vc.RenderCardOnBoard(vc.card);

        }
    }

    public override void MinionSummoned(Minion minion)
    {
        SoundyManager.Play(summonSound);

        if (minion.initialCost>=3)
        {
            cameraShake.ShakeFromHit(cameraShakeLevelWhenBigMinionSummoned);
        }

        if (minion.GetTile() != null && minion.GetDescription() != "")
        {
            GameObject vfxPrefab = summoningVFXPrefab;
            if (MenuControl.Instance.heroMenu.seasonsMode)
            {
                if (minion.cardTags.Contains(MenuControl.Instance.naughtyTag))
                {
                    vfxPrefab = seasonsNaughtySummoningVFXPrefab;
                }
                if (minion.cardTags.Contains(MenuControl.Instance.niceTag))
                {
                    vfxPrefab = seasonsNiceSummoningVFXPrefab;
                }
            }
            GameObject vfx = Instantiate(vfxPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
            vfx.transform.position = minion.GetTile().transform.position + Vector3.back * 10;
            Destroy(vfx, 5f);
        }

    }

    public override void HeroSummoned(Hero newHero)
    {
        if (MenuControl.Instance.battleMenu.currentRound > 0)
        {
            SoundyManager.Play(summonSound);

            if (newHero.GetTile() != null)
            {
                GameObject vfx = Instantiate(summoningVFXPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
                vfx.transform.position = newHero.GetTile().transform.position + Vector3.back * 10;
                Destroy(vfx, 5f);
            }
        }
    }

    public override void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {
        VisibleCard vc = hero.player.GetVisibleBoardCardForCard(hero);
        if (vc != null)
        {
            GameObject vfx = Instantiate(destroyVFXPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
            vfx.transform.position = vc.transform.position + Vector3.back * 10;
            Destroy(vfx, 5f);

        }

        SoundyManager.Play(destroySound);


    }

    public override void ManaChanged(Player player, int amountChanged, int previousAmount)
    {
        // create indicator
        int deltaAmount = player.currentMana - previousAmount;
        if (deltaAmount != 0 && (!MenuControl.Instance.battleMenu.usingIntentSystem || player == MenuControl.Instance.battleMenu.player1))
        {
            GameObject obj = Instantiate(manaIndicator, MenuControl.Instance.battleMenu.transform) as GameObject;
            obj.GetComponentInChildren<Text>().text = (deltaAmount >= 0 ? "+" : "") + deltaAmount.ToString();
            obj.transform.position = player.manaText.transform.position + Vector3.up * 0.3f;

            float timeToLive = MenuControl.Instance.battleMenu.GetPlaySpeed() * 3f;
            Destroy(obj, timeToLive);

            LeanTween.move(obj, obj.transform.position + Vector3.up * 0.3f, timeToLive).setEaseOutSine();
            LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(), 0f, timeToLive / 2).setDelay(timeToLive / 2);
            LeanTween.scale(obj, Vector3.one * 1.3f, timeToLive).setEaseOutSine();

            //SoundyManager.Play(manaGainedSound);
            //SoundyManager.Play(manaUsedSound);

        }
    }



    public override void PlayerShuffledDeck(Player player)
    {
        // create indicator
        int deltaAmount = player.cardsInDeck.Count;
        if (deltaAmount != 0)
        {
            for (int ii = 0; ii < 2; ii += 1)
            {
                GameObject obj = Instantiate(deckIndicator, MenuControl.Instance.battleMenu.transform) as GameObject;
                obj.GetComponentInChildren<Text>().text = (deltaAmount >= 0 ? "+" : "") + deltaAmount.ToString();

                obj.transform.position = (ii == 0 ? player.deckPileText.transform.position : player.discardPileText.transform.position) + (ii == 0 ? Vector3.right : Vector3.left) * 0.3f;

                float timeToLive = MenuControl.Instance.battleMenu.GetPlaySpeed() * 3f;
                Destroy(obj, timeToLive);

                LeanTween.move(obj, obj.transform.position + (ii == 0 ? Vector3.right : Vector3.left) * 0.3f, timeToLive).setEaseOutSine();
                LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(), 0f, timeToLive / 2).setDelay(timeToLive / 2);
                LeanTween.scale(obj, Vector3.one * 1.3f, timeToLive).setEaseOutSine();

                deltaAmount = -deltaAmount;
            }

            SoundyManager.Play(deckShuffledSound);
        }
    }

    public void CardsDrawnEffect(Player player, int amount)
    {
        // create indicator
        int deltaAmount = -amount;
        if (deltaAmount != 0 && !(player == MenuControl.Instance.battleMenu.playerAI && MenuControl.Instance.battleMenu.usingIntentSystem))
        {
            GameObject obj = Instantiate(deckIndicator, MenuControl.Instance.battleMenu.transform) as GameObject;
            obj.GetComponentInChildren<Text>().text = (deltaAmount >= 0 ? "+" : "") + deltaAmount.ToString();
            obj.transform.position = player.deckPileText.transform.position + Vector3.up * 0.3f;

            float timeToLive = MenuControl.Instance.battleMenu.GetPlaySpeed() * 3f;
            Destroy(obj, timeToLive);

            LeanTween.move(obj, obj.transform.position + Vector3.up * 0.3f, timeToLive).setEaseOutSine();
            LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(), 0f, timeToLive / 2).setDelay(timeToLive / 2);
            LeanTween.scale(obj, Vector3.one * 1.3f, timeToLive).setEaseOutSine();

        }
    }

    public void CardsDiscardedEffect(Player player, int amount)
    {
        // create indicator
        int deltaAmount = amount;
        if (deltaAmount != 0 && !(player == MenuControl.Instance.battleMenu.playerAI && MenuControl.Instance.battleMenu.usingIntentSystem))
        {
            GameObject obj = Instantiate(deckIndicator, MenuControl.Instance.battleMenu.transform) as GameObject;
            obj.GetComponentInChildren<Text>().text = (deltaAmount >= 0 ? "+" : "") + deltaAmount.ToString();
            obj.transform.position = player.discardPileText.transform.position + Vector3.up * 0.3f;

            float timeToLive = MenuControl.Instance.battleMenu.GetPlaySpeed() * 3f;
            Destroy(obj, timeToLive);

            LeanTween.move(obj, obj.transform.position + Vector3.up * 0.3f, timeToLive).setEaseOutSine();
            LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(), 0f, timeToLive / 2).setDelay(timeToLive / 2);
            LeanTween.scale(obj, Vector3.one * 1.3f, timeToLive).setEaseOutSine();

        }

        SoundyManager.Play(discardSound);
    }

    public override void GameEnded(bool victory)
    {
        Player player = MenuControl.Instance.battleMenu.player1;
        if (victory)
        {
            player = MenuControl.Instance.battleMenu.playerAI;
        }

        for (int ii = 0; ii < player.cardsOnBoard.Count; ii += 1)
        {
            Card card = player.cardsOnBoard[ii];

            LeanTween.delayedCall(0.5f + (ii * MenuControl.Instance.battleMenu.GetPlaySpeed() * 4f / player.cardsOnBoard.Count), () =>
            {
                VisibleCard vc = card.player.GetVisibleBoardCardForCard(card);
                if (vc != null)
                {
                    Vector3 pos = vc.card.player.discardPileText.transform.position;

                    GameObject vCToDiscard = Instantiate(vc.gameObject, vc.transform.parent) as GameObject;
                    vCToDiscard.transform.position = vc.transform.position;
                    vCToDiscard.transform.localScale = vc.transform.localScale;
                    LeanTween.move(vCToDiscard, pos, MenuControl.Instance.battleMenu.GetPlaySpeed() * 1.5f).setEaseOutSine().setDestroyOnComplete(true);
                    LeanTween.scale(vCToDiscard, Vector3.one * 0.5f, MenuControl.Instance.battleMenu.GetPlaySpeed());
                    LeanTween.rotateAround(vCToDiscard, Vector3.forward, 720f, MenuControl.Instance.battleMenu.GetPlaySpeed() * 1.5f);

                    GameObject vfx = Instantiate(destroyVFXPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
                    vfx.transform.position = vc.transform.position;
                    Destroy(vfx, 5f);

                    SoundyManager.Play(destroySound);
                    SoundyManager.Play(((Unit)vc.card).deathSound);
                    vc.Hide();


                }
            });
        }


    }

    public override void UnitChangedPower(Unit unit, Ability ability, int oldValue)
    {
        VisibleCard vc = unit.player.GetVisibleBoardCardForCard(unit);
        if (vc != null)
        {
            vc.powerTextBoard.transform.parent.localScale = Vector3.one;
            LeanTween.scale(vc.powerTextBoard.transform.parent.gameObject, Vector3.one * 1.5f, MenuControl.Instance.battleMenu.GetPlaySpeed() / 2f).setLoopPingPong(2).setEaseInOutSine();

        }

        if (unit.currentPower >= oldValue)
        {
            SoundyManager.Play(changePositiveSound);
        }
        else
        {
            SoundyManager.Play(changeNegativeSound);
        }
    }
    
    public override void CardChangedInitialManaCost(Card card, Ability ability, int oldValue)
    {
        VisibleCard vc = card.player.GetVisibleBoardCardForCard(card);
        if (vc != null)
        {
            vc.powerTextBoard.transform.parent.localScale = Vector3.one;
            LeanTween.scale(vc.costText.transform.parent.gameObject, Vector3.one * 1.5f, MenuControl.Instance.battleMenu.GetPlaySpeed() / 2f).setLoopPingPong(2).setEaseInOutSine();

        }

        if (card.initialCost < oldValue)
        {
            SoundyManager.Play(changePositiveSound);
        }
        else
        {
            SoundyManager.Play(changeNegativeSound);
        }
    }

    public override void UnitChangedCurrentHP(Unit unit, Ability ability, int oldValue)
    {
        VisibleCard vc = unit.player.GetVisibleBoardCardForCard(unit);
        if (vc != null)
        {
            vc.hPTextBoard.transform.parent.localScale = Vector3.one;
            LeanTween.scale(vc.hPTextBoard.transform.parent.gameObject, Vector3.one * 1.5f, MenuControl.Instance.battleMenu.GetPlaySpeed() / 2f).setLoopPingPong(2).setEaseInOutSine();

        }

        if (unit.currentHP >= oldValue)
        {
            SoundyManager.Play(changePositiveSound);
        }
        else
        {
            SoundyManager.Play(changeNegativeSound);
        }
    }


    public override void UnitChangedActions(Unit unit, Ability ability, int oldValue)
    {
        VisibleCard vc = unit.player.GetVisibleBoardCardForCard(unit);
        if (vc != null)
        {
            GameObject vfx = Instantiate(summoningVFXPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
            vfx.transform.position = unit.GetTile().transform.position + Vector3.back * 10;
            Destroy(vfx, 5f);

        }

        if (unit.remainingActions >= oldValue)
        {
            SoundyManager.Play(changePositiveSound);
        }
        else
        {
            SoundyManager.Play(changeNegativeSound);
        }
    }

    public override void UnitChangedMoves(Unit unit, Ability ability, int oldValue)
    {
        VisibleCard vc = unit.player.GetVisibleBoardCardForCard(unit);
        if (vc != null)
        {
            GameObject vfx = Instantiate(summoningVFXPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
            vfx.transform.position = unit.GetTile().transform.position + Vector3.back * 10;
            Destroy(vfx, 5f);

        }

        if (unit.remainingMoves >= oldValue)
        {
            SoundyManager.Play(changePositiveSound);
        }
        else
        {
            SoundyManager.Play(changeNegativeSound);
        }
    }

    public void ShowTextOverTile(string stringToShow, Color color, Tile targetTile)
    {
        if (targetTile == null) return;
        
        GameObject obj = Instantiate(manaIndicator, MenuControl.Instance.battleMenu.transform) as GameObject;
        obj.GetComponentInChildren<Text>().text = stringToShow;
        obj.GetComponentInChildren<Text>().color = color;
        obj.transform.position = targetTile.transform.position;

        float timeToLive = MenuControl.Instance.battleMenu.GetPlaySpeed() * 3f;
        Destroy(obj, timeToLive);

        LeanTween.move(obj, obj.transform.position + Vector3.up * 0.3f, timeToLive).setEaseOutSine();
        LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(), 0f, timeToLive / 2).setDelay(timeToLive / 2);
        LeanTween.scale(obj, Vector3.one * 1.3f, timeToLive).setEaseOutSine();
    }

    public List<Tile> GetAllEmptyTiles()
    {
        List<Tile> returnTiles = new List<Tile>();
        foreach (Tile tile in tiles)
        {
            if (tile.isMoveable())
            {
                returnTiles.Add(tile);
            }
        }

        return returnTiles;
    }
    
    public List<int> GetAllEmptyTileIndex()
    {
        List<int> returnCoordinates = new List<int>();
        int i = 0;
        foreach (Tile tile in tiles)
        {
            if (tile.isMoveable())
            {
                returnCoordinates.Add(i);
            }

            i++;
        }

        return returnCoordinates;
    }

}
