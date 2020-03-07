using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static void ExitGame() {
        Application.Quit();
    }

    public static void StartMainScene() {
        SceneManager.LoadScene("MainScene");
    }

    public static void LoadTitleScene() {
        SceneManager.LoadScene("TitleScene");
    }
}
