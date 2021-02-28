using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager_ : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    bool isLoading = false;

    public void NextScene() {
        isLoading = true;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene() {
        yield return new WaitForSeconds(delayTime);
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void Quit() {
        if (isLoading) return;
        Application.Quit();
        print("QUIT");
    }
}
  