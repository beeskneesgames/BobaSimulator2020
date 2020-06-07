using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {
    public SceneFader sceneFader;

    public void PlayInstructionsSound() {
        AudioManager.Instance.PlayPaper();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            sceneFader.FadeOut(() => {
                GameManager.StartTitleScene();
            });
        }
    }
}
