﻿using UnityEngine;

public class IceCatcher : MonoBehaviour {
    public IcePhase icePhase;

    private int iceCount = 0;
    private int IceCount {
        get {
            return iceCount;
        }

        set {
            iceCount = value;
            Globals.iceCount = IceCount;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Ice ice = other.GetComponent<Ice>();

        if (ice != null && !ice.IsCaught) {
            ice.FallIntoCup(GetComponentInParent<CupController>());
            IceCount++;

            if (icePhase.ShouldEndEarly()) {
                icePhase.EndPhaseEarly();
            }
        }
    }
}
