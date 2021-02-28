using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Musics")]
    [SerializeField] AudioClip preGameMusic;
    [SerializeField] AudioClip gameMusic;

    [Header("SFX")]
    [SerializeField] AudioClip startSound;
    [SerializeField] AudioClip quitSound;
    [SerializeField] AudioClip mainMenuSound;
    [SerializeField] AudioClip restartSound;

    AudioSource audioSource;

    bool isLoading = false;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        if (preGameMusic)
            audioSource.clip = preGameMusic;
        audioSource.Play();
    }

    void Start() {
        
    }

    public void StartSound() {
        if (!startSound) return;
        isLoading = true;
        audioSource.PlayOneShot(startSound);
        StartCoroutine(FadeOut(audioSource, 1.5f, .2f));
    }

    public void QuitSound()
    {
        if (isLoading || !quitSound) return;
        audioSource.PlayOneShot(quitSound);
    }

    public void RestartSound() {
        if (!restartSound) return;
        audioSource.PlayOneShot(restartSound);
    }

    public void MainMenuSound() {
        if (!mainMenuSound) return;
        audioSource.PlayOneShot(mainMenuSound);
    }

    public void SetGameMusic() {
        audioSource.clip = gameMusic;
        audioSource.Play();
    }

    IEnumerator FadeOut(AudioSource audioSource, float maxTime, float minVolume) {
        float time = 0;
        while (audioSource.volume > 0) {
            yield return new WaitForSeconds(.1f);
            time+=.1f;
            float perc = time/maxTime;
            audioSource.volume = Mathf.Lerp(1, minVolume, perc);
        }
    }

}
