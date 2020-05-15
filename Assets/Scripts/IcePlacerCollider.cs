using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacerCollider : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "LiquidFillClippingPlane") {
            GetComponentInParent<IcePlacer>().StartFloating();
            enabled = false;
        }
    }
}
