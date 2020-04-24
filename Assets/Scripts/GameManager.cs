using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {
    public static void ExitGame() {
        Application.Quit();
    }

    public static void StartCreditsScene() {
        SceneManager.LoadScene("CreditsScene");
    }

    public static void StartMainScene() {
        SceneManager.LoadScene("MainScene");
    }

    public static void StartTitleScene() {
        SceneManager.LoadScene("TitleScene");
    }
}
