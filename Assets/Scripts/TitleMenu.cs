using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    public Toggle debugToggle;

    private void Start() {
        Debugger.Instance.IsOn = debugToggle.isOn;
    }

    public void StartGame() {
        GameManager.StartMainScene();
    }

    public void ExitGame() {
        GameManager.ExitGame();
    }

    public void SetDebug(bool isDebug) {
        Debugger.Instance.IsOn = isDebug;
    }
}
