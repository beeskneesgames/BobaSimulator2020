using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba : MonoBehaviour {
    private bool manuallyFalling;
    private Vector3 targetPosition;

    private void Update() {
        if (manuallyFalling) {
            // TODO: lerp to targetPosition
            transform.localPosition = targetPosition;
            manuallyFalling = false;
        }
    }

    public void FallIntoCup(CupController cup) {
        manuallyFalling = true;
        BobaPlacer bobaPlacer = cup.GetComponentInChildren<BobaPlacer>();
        transform.parent = bobaPlacer.transform;

        Destroy(GetComponent<Rigidbody>());
        GetComponent<Collider>().enabled = false;

        targetPosition = bobaPlacer.PopPosition();
    }
}
