using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {
    public SceneFader sceneFader;

    private void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            FadeToTitleScene();
        }
    }

    public void OnArmHidden() {
        FadeToTitleScene();
    }

    private void FadeToTitleScene() {
        sceneFader.FadeOut(() => {
            GameManager.StartTitleScene();
        });
    }
}
