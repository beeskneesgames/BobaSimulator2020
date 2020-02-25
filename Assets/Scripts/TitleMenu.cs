using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    public Toggle debugToggle;

    private void Start() {
        GlobalVariables.isDebug = debugToggle.isOn;
    }

    public void StartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void SetDebug(bool isDebug) {
        GlobalVariables.isDebug = isDebug;
    }
}
