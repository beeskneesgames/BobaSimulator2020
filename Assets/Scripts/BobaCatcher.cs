using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaCatcher : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Boba boba = other.GetComponent<Boba>();

        if (boba != null) {
            boba.FallIntoCup(GetComponentInParent<CupController>());
        }
    }
}
