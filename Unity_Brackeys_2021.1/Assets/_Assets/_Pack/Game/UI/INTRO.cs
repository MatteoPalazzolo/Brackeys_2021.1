using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTRO : MonoBehaviour
{
    GameObject HUD;
    AudioManager audioManager;
    Clock clock;

    void Awake() {
        HUD = FindObjectOfType<HUD>().gameObject;
        audioManager = FindObjectOfType<AudioManager>();
        clock = FindObjectOfType<Clock>();
        Time.timeScale = 0;
    }

    void Start() {
        HUD.SetActive(false);
    }

    public void StartGame() {
        //StartCoroutine(StartGameCorutine());
        Time.timeScale = 1;
        audioManager.SetGameMusic();

        HUD.SetActive(true);
        gameObject.SetActive(false);

        clock.StartCounter();
    }

}
