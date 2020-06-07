using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    public SceneFader sceneFader;

    private void Start() {
        Debugger.Instance.IsOn = false;
    }

    public void StartGame() {
        sceneFader.FadeOut(() => {
            GameManager.StartMainScene();
        });
    }

    public void ShowCredits() {
        GameManager.StartCreditsScene();
    }

    public void ExitGame() {
        Application.Quit();
    }
}
