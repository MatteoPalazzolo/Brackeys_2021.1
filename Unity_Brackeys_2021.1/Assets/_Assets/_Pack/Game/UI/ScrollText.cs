using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ScrollText : MonoBehaviour 
{
    [Header("Buttons")]
    [SerializeField] Button startBTN;
    [SerializeField] Button nextBTN;
    [SerializeField] Button previousBTN;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] [TextArea(5,10)] string[] dialogues;
    string currentDialogue;
    int currendIndex = 0;

    private void Start() {
        UpdateText();
        UpdateActive();
    }

    public void NextDialogue() {
        //if (currendIndex + 1 >= dialogues.Length) return;
        currendIndex += 1;
        UpdateText();
        UpdateActive();
    }

    public void PreviousDialogue() {
        //if (currendIndex - 1 < 0) return;
        currendIndex -= 1;
        UpdateText();
        UpdateActive();
    }

    void UpdateText() {
        currentDialogue = dialogues[currendIndex];
        text.text = currentDialogue;
    }

    void UpdateActive() {
        startBTN.interactable = (currendIndex == dialogues.Length-1);
        nextBTN.interactable = (currendIndex != dialogues.Length - 1);
        previousBTN.interactable = (currendIndex != 0);
    }

}
