using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawParabola : MonoBehaviour
{
    public Sprite dotPrefab; // 小圆点 prefab
    public Sprite arrowPrefab; // 箭头 prefab

    public List<GameObject> sprites = new List<GameObject>();
    GameObject instantiateSprite(Sprite mySprite)
    {
        GameObject newGameObject = new GameObject("MySprite");
        Image renderer = newGameObject.AddComponent<Image>();
        renderer.sprite = mySprite;
        newGameObject.transform.position = new Vector3(0, 0, 0);
        renderer.SetNativeSize();
        return newGameObject;
    }
    
    
    
    public List<GameObject> CreateParabola(Vector3 startPoint,Vector3 endPoint,int resolution)
    {
        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 position = MenuControl.Instance.adventureMenu.CalculateParabola(startPoint, endPoint, MenuControl.Instance.adventureMenu.parabolaHeight, t);

            // 如果是最后一个点，实例化箭头
            if (i == resolution)
            {
                GameObject arrow = instantiateSprite(arrowPrefab);
                arrow.transform.position = position;
                arrow.transform.SetParent(this.transform, false);

                // 设置箭头方向
                Vector3 direction = endPoint - (Vector3) sprites.LastItem().GetComponent<RectTransform>().anchoredPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                sprites.Add(arrow);
                //如果箭头的角度（angle）大于90度且小于-90度，flip y轴
                if (angle > 90 || angle < -90)
                {
                    arrow.transform.localScale = new Vector3(1, -1, 1);
                }
            }
            else
            {
                
                // 实例化小圆点并设置位置
                GameObject dot = instantiateSprite(dotPrefab);
                dot.transform.position = position;
                dot.transform.SetParent(this.transform, false);
                sprites.Add(dot);
            }
        }

        return sprites;
    }

    public void clearParabola()
    {
        foreach (var sprite in sprites)
        {
            Destroy(sprite);
        }
        sprites.Clear();
    }

}