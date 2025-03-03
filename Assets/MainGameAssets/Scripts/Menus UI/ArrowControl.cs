using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour {

	public Transform topArrow;
	public Transform middleArrow;
	public Transform baseArrow;

	public Vector2 basePos;
	public Vector2 pointerPos;
	public Vector2 cameraPos;
    GameObject obj = null;
    public bool forcedOnMode;

	// Use this for initialization
	void Start () {
		
	
		gameObject.SetActive (false);
	}

    public void DrawArrow(VisibleCard vc) {
        forcedOnMode = false;
        obj = vc.gameObject;

        gameObject.SetActive (true);
        Update();
    }
    public void DrawArrowBetween(VisibleCard vc1, VisibleCard vc2)
    {
        forcedOnMode = true;
        obj = vc1.gameObject;
        pointerPos = vc2.transform.position;

        gameObject.SetActive(true);
        Update();
    }

    public void HideArrow() { 
        gameObject.SetActive (false);
	}

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            basePos = obj.transform.position;
            basePos = new Vector2(basePos.x, basePos.y);

            if (!forcedOnMode)
                pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 startPos = basePos;
            Vector2 endPos = (startPos - pointerPos) * 0.4f / Vector2.Distance(startPos, pointerPos) + pointerPos;// + (cameraPos - pointerPos) * 30f / Vector3.Distance (cameraPos, pointerPos);

            if (Vector2.Distance(endPos, startPos) > 0f)
            {

                topArrow.position = endPos;
                baseArrow.position = startPos;
                middleArrow.position = startPos;

                Vector2 directionVec = endPos - startPos;

                // Construct a rotation as in the y+ case.
                Quaternion rotation = Quaternion.LookRotation(
                                          Vector3.forward,
                                          directionVec
                                      );

                // Apply a compensating rotation that twists x+ to y+ before the rotation above.
                baseArrow.transform.rotation = rotation * Quaternion.Euler(0, 0, 0);


                Quaternion rot = Quaternion.LookRotation(directionVec);


                topArrow.rotation = rot;

                middleArrow.localScale = new Vector3(1.5f, 1.5f, Vector2.Distance(endPos, startPos) / 2.0f);
                middleArrow.rotation = rot;
            }
            else
            {
                HideArrow();
            }
        }

    }
}
