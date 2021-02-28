using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    [Header("Settings")]
    public Settings settings;

    [Header("References")]
    public Transform player;
    public BellTarget bellTarget;
    public Transform childrensParent;
    public Transform UIParent;
    public GameObject UIPrefab;
    public Canvas canvas;
}
