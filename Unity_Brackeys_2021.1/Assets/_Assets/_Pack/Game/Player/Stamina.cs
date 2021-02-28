using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] Slider staminaBar;
    [SerializeField] Image staminaImage;

    Manager manager;
    Settings settings;
    Player player;

    public int stamina;
    bool restore = false;
    bool corutineTrigger = true;


    void Awake() {
        manager = FindObjectOfType<Manager>();
        settings = manager.settings;
        stamina = settings.maxStamina;
        player = manager.player.GetComponent<Player>();
    }

    void Update() {
        if (!player.isRunning && corutineTrigger) {
            corutineTrigger = false;
            StartCoroutine(RestoreCorutine());
        }

        if (player.isRunning) {
            corutineTrigger = true;
            restore = false;
            StopAllCoroutines();
        }

        if (restore)
            RestoreStamina();
        
    }

    public void DecreaseStamina() {
        int newStamina = stamina - settings.decreaseStamina;
        stamina = Mathf.Clamp(newStamina, 0, settings.maxStamina);
        UpdateBarValue();
        UpdateBarColor();
    }

    private void RestoreStamina() {
        int newStamina = stamina + settings.restoreStamina;
        stamina = Mathf.Clamp(newStamina, 0, settings.maxStamina);
        UpdateBarValue();
        UpdateBarColor();
    }

    private void UpdateBarValue() {
        if (!staminaBar) return;
        float perc = (float)stamina / settings.maxStamina;
        staminaBar.value = perc;
    }

    private void UpdateBarColor() {
        float perc = (float)stamina / settings.maxStamina;
        Color color;
        if (perc > .5f) {
            color = Color.Lerp(Color.yellow, Color.green, (perc-.5f)*2f);
        }
        else {
            color = Color.Lerp(Color.red, Color.yellow, perc*2f);
        }
        staminaImage.color = color;
    }
    
    IEnumerator RestoreCorutine() {
        yield return new WaitForSeconds(settings.preRestoreTime);
        restore = true;
    }
}
