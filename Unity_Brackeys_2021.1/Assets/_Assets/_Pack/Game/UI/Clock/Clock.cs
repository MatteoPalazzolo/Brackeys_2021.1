using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Animator clockIcon;
    AudioManager audioManager;
    EndGameMenu endGameMenu;
    Manager manager;
    Settings settings;

    public int time;
    int min;
    int sec;

    private void Awake() {
        endGameMenu = FindObjectOfType<EndGameMenu>();
        audioManager = FindObjectOfType<AudioManager>();
        manager = FindObjectOfType<Manager>();
        settings = manager.settings;
    }

    void Start() {
        time = settings.gameTimer;
        sec = time % 60;
        min = (time - sec) / 60;
        DisplayTime();
    }

    public void StartCounter() {
        StartCoroutine(Counter());
    }

    public void StopCounter() {
        StopAllCoroutines();
    }

    public void ResetCounter() {
        StopAllCoroutines();
        sec = time % 60;
        min = (time - sec) / 60;
    }

    IEnumerator Counter() {
        while (true) {
            yield return new WaitForSeconds(1f);
            time -= 1;
            if (time == 10) LastSeconds();
            CalculateTime();
            DisplayTime();
            if (time == 0) {
                TimeFinished();
                break;
            }
        }
    }

    void CalculateTime() {
        sec = time % 60;
        min = (time - sec) / 60;
    }

    void DisplayTime() {
        text.text = AddZero(min) + ":" + AddZero(sec);
    }

    string AddZero(int num) {
        if (num.ToString().Length <= 1)
            return "0" + num;
        else
            return num.ToString();
    }

    void LastSeconds() {
        clockIcon.SetTrigger("Shake");
        //audioManager.; drin drin
    }

    void TimeFinished() {
        endGameMenu.EndGame(EndType.Time);
    }
}
