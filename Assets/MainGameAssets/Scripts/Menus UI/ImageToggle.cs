using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : MonoBehaviour
{
    public Image imageToChange;
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject objectToObserve;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            imageToChange.sprite = objectToObserve.activeInHierarchy ? sprites[0] : sprites[1];
        }
    }
}
