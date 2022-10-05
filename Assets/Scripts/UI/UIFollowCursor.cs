using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCursor : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 mousePos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        FollowTheCusrsor();
    }

    void FollowTheCusrsor()
    {
        rectTransform.position = Input.mousePosition;
    }
}
