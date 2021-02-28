using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Emote_UI : MonoBehaviour
{
    public Transform target;

    GameObject exclamationIcon;
    GameObject dialogueIcon;
    TextMeshProUGUI text;

    bool isExclamRunning = false;
    IEnumerator exclamCorutine;
    bool isTextRunning = false;
    IEnumerator textCorutine;

    void Awake()
    {
        exclamationIcon = transform.GetChild(0).gameObject;
        dialogueIcon = transform.GetChild(1).gameObject;
        text = dialogueIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        SetValues();
        exclamationIcon.SetActive(false);
        dialogueIcon.SetActive(false);
    }

    void SetValues()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
            transform.GetChild(i).GetComponent<WorldToUI>().lookAt = target;
    }

    public void DisplayExclamMark(float time)
    {
        if (isExclamRunning)
        {
            StopCoroutine(exclamCorutine);
        }
        isExclamRunning = true;
        exclamCorutine = ExclamMarkCoorutine(time);
        StartCoroutine(exclamCorutine);
        isExclamRunning = false;
    }

    IEnumerator ExclamMarkCoorutine(float time)
    {
        if (!dialogueIcon.activeSelf)
            exclamationIcon.SetActive(true);
        yield return new WaitForSeconds(time);
        exclamationIcon.SetActive(false);
    }

    public void SetExclamationMark(bool val)
    {
        exclamationIcon.SetActive(val);
    }

    public void DisplayText(string text, float time)
    {
        if (isTextRunning)
        {
            StopCoroutine(textCorutine);
        }
        isTextRunning = true;
        textCorutine = TextCoorutine(text, time);
        StartCoroutine(textCorutine);
        isTextRunning = false;
    }

    IEnumerator TextCoorutine(string text, float time)
    {
        this.text.text = text;
        if (!dialogueIcon.activeSelf)
            dialogueIcon.SetActive(true);
        yield return new WaitForSeconds(time);
        dialogueIcon.SetActive(false);
        this.text.text = "";
    }

}