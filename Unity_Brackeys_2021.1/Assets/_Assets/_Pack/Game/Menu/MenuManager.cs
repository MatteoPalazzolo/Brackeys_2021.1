using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;

    private void Start() {
        if (!menu) return;
        menu.SetActive(false);
    }

    public void SwitchMenuState() {
        menu.SetActive(!menu.activeSelf);
        TriggerActive();
    }

    void TriggerActive() {
        if (menu.activeSelf) OnMenuEnable();
        else OnMenuDisable();
    }

    private void OnMenuEnable() {
        Time.timeScale = 0;
    }

    private void OnMenuDisable() {
        Time.timeScale = 1;
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartGame() {
        Time.timeScale = 1;
        int thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisSceneIndex);
    }
}
