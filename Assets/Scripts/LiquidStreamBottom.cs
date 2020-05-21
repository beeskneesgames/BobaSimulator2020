using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidStreamBottom : MonoBehaviour {
    private LiquidStream liquidStream;

    private void Start() {
        liquidStream = GetComponentInParent<LiquidStream>();
    }

    private void OnTriggerEnter(Collider other) {
        if (liquidStream && liquidStream.CurrentTransition == LiquidStream.Transition.In && other.tag == "CupBottom") {
            // We're colliding with the cup bottom, stop the transition early.
            liquidStream.StopTransitionIn();
        }
    }
}
