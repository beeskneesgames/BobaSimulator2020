using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacerCollider : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<ClippingPlane>()) {
            GetComponentInParent<IcePlacer>().StartFloating();
            enabled = false;
        }
    }
}
