using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToUI : MonoBehaviour
{
    [Header("Tweaks")]
    public Transform lookAt;

    Camera cam;
    RectTransform rectTransform;

    void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position);
        if (rectTransform.position != pos) {
            rectTransform.position = pos;
        }
    }
}
