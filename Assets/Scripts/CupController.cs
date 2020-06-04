using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour {

    private void Update() {
        if (Globals.isPaused) {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(
            mousePosition.x,
            transform.position.y,
            transform.position.z
        );
    }
}
