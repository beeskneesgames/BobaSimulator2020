using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    public GameObject menuContainer;

    private float oldTimeScale;

    private void Start() {
        oldTimeScale = Time.timeScale;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void OnDestroy() {
        Resume();
    }

    public void Resume() {
        isPaused = false;
        Time.timeScale = oldTimeScale;

        menuContainer.SetActive(false);
    }

    public void Pause() {
        isPaused = true;
        oldTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;

        menuContainer.SetActive(true);
    }

    public void Restart() {
        GameManager.StartMainScene();
    }

    public void ExitGame() {
        GameManager.StartTitleScene();
    }
}
