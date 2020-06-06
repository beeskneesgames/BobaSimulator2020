using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {
    public static void ExitGame() {
        Globals.orderCount = 0;
        SceneManager.LoadScene("TitleScene");
    }

    public static void StartCreditsScene() {
        SceneManager.LoadScene("CreditsScene");
    }

    public static void StartIntroCutScene() {
        SceneManager.LoadScene("IntroCutScene");
    }

    public static void StartMainScene() {
        SceneManager.LoadScene("MainScene");
    }

    public static void StartTitleScene() {
        SceneManager.LoadScene("TitleScene");
    }

}
