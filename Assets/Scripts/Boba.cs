using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba : MonoBehaviour {
    private bool manuallyFalling;

    private void Update() {
        if (manuallyFalling) {
            // TODO: Fix boba fall speed
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - Time.deltaTime,
                transform.position.z
            );
        }
    }

    public void FallIntoCup(CupController cup) {
        manuallyFalling = true;
        transform.parent = cup.transform;

        Destroy(GetComponent<Rigidbody>());
    }
}
