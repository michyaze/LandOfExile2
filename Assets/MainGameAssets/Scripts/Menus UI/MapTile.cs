// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;
//
// public enum MapTileDirection
// {
//     Up,Left,Right,Down,None
// }
//
// public class MapTile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
// {
//     //Visual
//     public Transform room;
//     public Image iconImage;
//     public Image encounterIconImage;
//     public GameObject unexploredIcon;
//     public GameObject flareStoneIcon;
//
//     public Sprite randomEventIcon;
//     public Text toolTipText;
//
//     public Image encounterIconFrameImage;
//     public Sprite normalEncounterFrameSprite;
//     public Sprite eliteBossEncounterFrameSprite;
//
//     public Sprite alternateCompletedIconSprite;
//
//     //Data
//     public string directions = "aaaa"; //aaaa  baaa is left abaa is up aaba is right aaab is down
//     public int posX;
//     public int posY;
//     public int adventureItemIndex = -1;
//     public bool revealed;
//     public bool skipped;
//     float flickerTimer;
//
//     public bool HasDirection(MapTileDirection direction)
//     {
//         if (direction == MapTileDirection.Left)
//             return directions[0].ToString() == "b";
//         if (direction == MapTileDirection.Up)
//             return directions[1].ToString() == "b";
//         if (direction == MapTileDirection.Right)
//             return directions[2].ToString() == "b";
//         if (direction == MapTileDirection.Down)
//             return directions[3].ToString() == "b";
//
//         return false;
//     }
//
//     public List<MapTileDirection> GetDirections()
//     {
//         List<MapTileDirection> mapTileDirections = new List<MapTileDirection>();
//         if (HasDirection(MapTileDirection.Left))
//             mapTileDirections.Add(MapTileDirection.Left);
//         if (HasDirection(MapTileDirection.Up))
//             mapTileDirections.Add(MapTileDirection.Up);
//         if (HasDirection(MapTileDirection.Right))
//             mapTileDirections.Add(MapTileDirection.Right);
//         if (HasDirection(MapTileDirection.Down))
//             mapTileDirections.Add(MapTileDirection.Down);
//         return mapTileDirections;
//     }
//
//     public void SetDirection(MapTileDirection direction, bool hasDirection)
//     {
//         if (direction == MapTileDirection.Left)
//             directions = (hasDirection ? "b" : "a") + directions[1].ToString() + directions[2].ToString() + directions[3].ToString();
//         if (direction == MapTileDirection.Up)
//             directions = directions[0].ToString() + (hasDirection ? "b" : "a") + directions[2].ToString() + directions[3].ToString();
//         if (direction == MapTileDirection.Right)
//             directions = directions[0].ToString() + directions[1].ToString() + (hasDirection ? "b" : "a") + directions[3].ToString();
//         if (direction == MapTileDirection.Down)
//             directions = directions[0].ToString() + directions[1].ToString() + directions[2].ToString() + (hasDirection ? "b" : "a");
//     }
//
//     public List<MapTileDirection> SetRandomValidDirections(int number)
//     {
//         directions = "aaaa";
//
//         List<MapTileDirection> mapTileDirections = new List<MapTileDirection>();
//         while (mapTileDirections.Count < number)
//         {
//             int randomInt = Random.Range(0, 4);
//             if (randomInt == 0 && !mapTileDirections.Contains(MapTileDirection.Left))
//                 mapTileDirections.Add(MapTileDirection.Left);
//             if (randomInt == 1 && !mapTileDirections.Contains(MapTileDirection.Up))
//                 mapTileDirections.Add(MapTileDirection.Up);
//             if (randomInt == 2 && !mapTileDirections.Contains(MapTileDirection.Right))
//                 mapTileDirections.Add(MapTileDirection.Right);
//             if (randomInt == 3 && !mapTileDirections.Contains(MapTileDirection.Down))
//                 mapTileDirections.Add(MapTileDirection.Down);
//
//         }
//
//         foreach (MapTileDirection mapTileDirection in mapTileDirections)
//         {
//             SetDirection(mapTileDirection, true);
//         }
//
//         return mapTileDirections;
//
//     }
//
//     public MapTile GetTileInDirection(MapTileDirection mapTileDirection)
//     {
//         // if (mapTileDirection == MapTileDirection.Left)
//         //     return MenuControl.Instance.adventureMenu.GetMaptileAt(posX - 1, posY);
//         // if (mapTileDirection == MapTileDirection.Up)
//         //     return MenuControl.Instance.adventureMenu.GetMaptileAt(posX, posY + 1);
//         // if (mapTileDirection == MapTileDirection.Right)
//         //     return MenuControl.Instance.adventureMenu.GetMaptileAt(posX + 1, posY);
//         // if (mapTileDirection == MapTileDirection.Down)
//         //     return MenuControl.Instance.adventureMenu.GetMaptileAt(posX, posY - 1);
//
//         return null;
//     }
//
//     public bool CanBeRevealed()
//     {
//         foreach (MapTileDirection direction in GetDirections())
//         {
//             MapTile otherTile = GetTileInDirection(direction);
//             if (otherTile != null && otherTile.revealed)
//             {
//
//                 if (otherTile.GetAdventureItem() == null) return true;
//                 else if (MenuControl.Instance.adventureMenu.adventureItemCompletions[otherTile.adventureItemIndex]) return true;
//                 else if (otherTile.GetAdventureItem().alwaysOpen) return true;
//                 else if (otherTile.skipped) return true;
//             }
//         }
//
//         return false;
//     }
//
//     public AdventureItem GetAdventureItem()
//     {
//         if (adventureItemIndex == -1) return null;
//
//         return MenuControl.Instance.adventureMenu.adventureItems[adventureItemIndex];
//     }
//
//     public void Render()
//     {
//         if (I2.Loc.LocalizationManager.CurrentLanguage != MenuControl.Languages.English.ToString())
//         {
//             toolTipText.font = MenuControl.Instance.GetSafeFont();
//         }
//
//         GetComponent<RectTransform>().anchoredPosition = new Vector2(posX * 400, posY * 400);
//
//         room.gameObject.SetActive(revealed);
//
//         if (revealed)
//         {
//             gameObject.SetActive(true);
//
//             room.GetChild(0).GetComponent<Image>().sprite = MenuControl.Instance.areaMenu.currentArea.mapRoomSprites[MenuControl.Instance.adventureMenu.mapTiles.IndexOf(this) % MenuControl.Instance.areaMenu.currentArea.mapRoomSprites.Count];
//             if (MenuControl.Instance.useAlternateSprites)
//             {
//                 if (MenuControl.Instance.areaMenu.currentArea.alternateMapRoomSprites.Count > 0)
//                 {
//                     room.GetChild(0).GetComponent<Image>().sprite = MenuControl.Instance.areaMenu.currentArea.alternateMapRoomSprites[MenuControl.Instance.adventureMenu.mapTiles.IndexOf(this) % MenuControl.Instance.areaMenu.currentArea.alternateMapRoomSprites.Count];
//                 }
//             }
//
//             room.GetChild(4).gameObject.SetActive(false);
//             room.GetChild(1).gameObject.SetActive(false);
//             room.GetChild(2).gameObject.SetActive(false);
//             room.GetChild(3).gameObject.SetActive(false);
//
//             foreach (MapTileDirection direction in GetDirections())
//             {
//                 if (direction == MapTileDirection.Left)
//                     room.GetChild(1).gameObject.SetActive(true);
//                 if (direction == MapTileDirection.Right)
//                     room.GetChild(3).gameObject.SetActive(true);
//                 if (direction == MapTileDirection.Up)
//                     room.GetChild(2).gameObject.SetActive(true);
//                 if (direction == MapTileDirection.Down)
//                     room.GetChild(4).gameObject.SetActive(true);
//             }
//
//             iconImage.gameObject.SetActive(adventureItemIndex != -1);
//             encounterIconImage.transform.parent.gameObject.SetActive(false);
//             if (adventureItemIndex != -1)
//             {
//                 if (GetAdventureItem() is AdventureItemRandomEvent)
//                 {
//                     iconImage.sprite = randomEventIcon;
//                     toolTipText.text = MenuControl.Instance.GetLocalizedString("Random Event");
//                     iconImage.color = MenuControl.Instance.adventureMenu.adventureItemCompletions[adventureItemIndex] ? new Color(64f / 255f, 64f / 255f, 64f / 255f) : Color.white;
//                     iconImage.gameObject.SetActive(true);
//                 }
//                 else
//                 {
//                     if (GetAdventureItem() is AdventureItemEncounter)
//                     {
//                         iconImage.gameObject.SetActive(false);
//                         encounterIconImage.transform.parent.gameObject.SetActive(true);
//                         if (MenuControl.Instance.heroMenu.ascensionMode >= 12)
//                         {
//                             encounterIconImage.sprite = MenuControl.Instance.adventureMenu.hiddenEncounterSprite;
//                         }
//                         else
//                         {
//                             encounterIconImage.sprite = GetAdventureItem().GetSprite();
//                         }
//
//                         if (((AdventureItemEncounter)GetAdventureItem()).signPosts.Contains("Elite") || ((AdventureItemEncounter)GetAdventureItem()).isBoss)
//                             encounterIconFrameImage.sprite = eliteBossEncounterFrameSprite;
//                         else
//                             encounterIconFrameImage.sprite = normalEncounterFrameSprite;
//
//                         toolTipText.text = "";
//                         if (MenuControl.Instance.heroMenu.ascensionMode < 12)
//                         {
//
//                             toolTipText.text += ((AdventureItemEncounter)GetAdventureItem()).GetHero().GetName() + "\n";
//                             if (I2.Loc.LocalizationManager.CurrentLanguage == MenuControl.Languages.German.ToString())
//                             {
//                                 toolTipText.text = toolTipText.text.Replace("-", "- ");
//                             }
//                         }
//                         toolTipText.text +=  "Lv." + ((AdventureItemEncounter)GetAdventureItem()).level;
//                         toolTipText.text += " - XP." + ((AdventureItemEncounter)GetAdventureItem()).GetAdjustedXP();
//                         toolTipText.text += ((AdventureItemEncounter)GetAdventureItem()).isBoss ? "\n<color=red>" + MenuControl.Instance.GetLocalizedString("Boss")+"</color>" : "";
//                         foreach (string textString in ((AdventureItemEncounter)GetAdventureItem()).signPosts)
//                         {
//                             toolTipText.text += "\n<color=red>" + MenuControl.Instance.GetLocalizedString(textString) + "</color>";
//                         }
//
//                         encounterIconImage.color = MenuControl.Instance.adventureMenu.adventureItemCompletions[adventureItemIndex] ? new Color(64f / 255f, 64f / 255f, 64f / 255f) : Color.white;
//                         encounterIconImage.transform.parent.GetComponent<Image>().color = MenuControl.Instance.adventureMenu.adventureItemCompletions[adventureItemIndex] ? new Color(64f / 255f, 64f / 255f, 64f / 255f) : Color.white;
//
//                         if (MenuControl.Instance.useAlternateSprites)
//                         {
//                             encounterIconImage.transform.GetChild(0).GetComponent<Image>().sprite = alternateCompletedIconSprite;
//                         }
//
//                         encounterIconImage.transform.GetChild(0).gameObject.SetActive(MenuControl.Instance.adventureMenu.adventureItemCompletions[adventureItemIndex]);
//                     }
//                     else
//                     {
//                         iconImage.sprite = GetAdventureItem().GetSprite();
//                         toolTipText.text = GetAdventureItem().GetName();
//                         iconImage.color = MenuControl.Instance.adventureMenu.adventureItemCompletions[adventureItemIndex] ? new Color(64f / 255f, 64f / 255f, 64f / 255f) : Color.white;
//                         iconImage.gameObject.SetActive(true);
//                     }
//
//                 }
//             }
//             unexploredIcon.SetActive(false);
//             flareStoneIcon.SetActive(false);
//         }
//         else
//         {
//             //flareStoneIcon.SetActive(CanBeRevealed() && MenuControl.Instance.adventureMenu.flareStoneMode);
//             //unexploredIcon.SetActive(CanBeRevealed() && !MenuControl.Instance.adventureMenu.flareStoneMode);
//             //gameObject.SetActive(CanBeRevealed());
//             unexploredIcon.SetActive(false);
//             flareStoneIcon.SetActive(false);
//             gameObject.SetActive(false);
//         }
//         toolTipText.transform.parent.gameObject.SetActive(false);
//     }
//
//     public void OnPointerDown(PointerEventData eventData)
//     {
//         if (!room.gameObject.activeInHierarchy) return;
//
//         if (eventData.button == PointerEventData.InputButton.Middle)
//             return;
//         else if (eventData.button == PointerEventData.InputButton.Right)
//             return;
//
//
//         GetComponentInParent<AdventureMenu>().SelectMapTile(this);
//
//     }
//
//     public void OnPointerUp(PointerEventData eventData)
//     {
//         if (!room.gameObject.activeInHierarchy) return;
//
//         if (eventData.button == PointerEventData.InputButton.Middle)
//             return;
//         else if (eventData.button == PointerEventData.InputButton.Right)
//             return;
//
//
//         GetComponentInParent<AdventureMenu>().DeSelectMapTile(this);
//
//     }
//
//     public void OnPointerClick(PointerEventData eventData)
//     {
//         if (!flareStoneIcon.activeInHierarchy && !unexploredIcon.activeInHierarchy && !room.gameObject.activeInHierarchy) return;
//
//         if (eventData.button == PointerEventData.InputButton.Middle)
//             return;
//         else if (eventData.button == PointerEventData.InputButton.Right)
//             return;
//
//         GetComponentInParent<AdventureMenu>().ClickMapTile(this);
//     }
//
//     public void OnPointerEnter(PointerEventData eventData)
//     {
//         LeanTween.cancel(toolTipText.transform.parent.gameObject);
//         toolTipText.transform.parent.GetComponent<CanvasGroup>().alpha = 0f;
//         LeanTween.alphaCanvas(toolTipText.transform.parent.GetComponent<CanvasGroup>(), 1f, 0.25f);
//         toolTipText.transform.parent.gameObject.SetActive(iconImage.gameObject.activeInHierarchy || encounterIconImage.gameObject.activeInHierarchy);
//     }
//
//     public void OnPointerExit(PointerEventData eventData)
//     {
//         toolTipText.transform.parent.gameObject.SetActive(false);
//     }
// }
