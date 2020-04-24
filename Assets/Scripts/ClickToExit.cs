using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToExit : MonoBehaviour {
    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            GameManager.StartTitleScene();
        }
    }
}
