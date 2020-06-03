using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour {
    public void CreditsAnimationEnded() {
        GameManager.StartTitleScene();
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.StartTitleScene();
        }
    }
}
