using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public enum EndType { Time, MaxChild }
public class EndGameMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject OUTRO;
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI endTypeText;
    [SerializeField] TextMeshProUGUI strenghtText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Colors")]
    [SerializeField] Color winBackgroundColor = new Color(0.4196078f, 0.8705882f,1);
    [SerializeField] Color loseBackgroundColor = new Color(1, 0.2877358f, 0.2877358f);

    Clock clock;
    BellTarget bellTarget;

    EndType endType;
    int strength;
    int endTime;
    int score;
    bool win;

    void Awake() {
        clock = FindObjectOfType<Clock>();
        bellTarget = FindObjectOfType<BellTarget>();
        OUTRO.SetActive(false);
    }

    public void EndGame(EndType endType) {
        HUD.SetActive(false);
        OUTRO.SetActive(true);
        Time.timeScale = 0;
        CalculateScore(endType);
        UpdateDisplay();
    }

    void CalculateScore(EndType endType) {
        this.endType = endType;
        this.endTime = GetTime();
        this.strength = GetStrenght();
        score = (strength + endTime);
        win = (strength >= 50);
    }

    void UpdateDisplay() {
        UpdateWinText();
        UpdateEndText();
        UpdateScores();
        UpdateColor();
    }

    void UpdateWinText() {
        if (win) winText.text = "YOU WON!!! :-)";
        else winText.text = "YOU LOST! :-(";
    }

    void UpdateEndText() {
        if (endType == EndType.Time) endTypeText.text = "The time is over!!!";
        else if (endType == EndType.MaxChild) endTypeText.text = "You catched 6 children!!!";
    }

    void UpdateScores() {
        strenghtText.text = "Strength: " + strength;
        timeText.text = "Remaining Time: " + endTime;
        scoreText.text = "Score: " + score;
    }

    void UpdateColor() {
        if (win) background.color = winBackgroundColor;
        else background.color = loseBackgroundColor;
    }

    int GetTime() {
        return clock.time;
    }

    int GetStrenght() {
        return bellTarget.totalStrenght;
    }

}
