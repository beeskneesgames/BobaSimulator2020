using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    private void Start() {
        Debugger.Instance.IsOn = false;
    }

    public void StartGame() {
        GameManager.StartMainScene();
    }

    public void ShowCredits() {
        GameManager.StartCreditsScene();
    }
}
