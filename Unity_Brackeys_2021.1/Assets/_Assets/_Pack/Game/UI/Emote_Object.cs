using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote_Object : MonoBehaviour
{
    [SerializeField] Vector3 scale = Vector3.one;
    [SerializeField] Transform emoteTarget;
    [SerializeField] float displayTime;
    [SerializeField] string text;

    [HideInInspector] public Emote_UI emoteUI;

    Manager manager;

    void Awake() {
        manager = FindObjectOfType<Manager>();
    }

    void Start() {
        CrateUI();
    }

    private void CrateUI()
    {
        emoteUI = Instantiate(manager.UIPrefab, manager.UIParent).GetComponent<Emote_UI>();
        emoteUI.transform.localScale = scale;
        emoteUI.target = transform.GetChild(1);
    }

    public void DisplayText() {
        emoteUI.DisplayText(text, displayTime);
    }
}
