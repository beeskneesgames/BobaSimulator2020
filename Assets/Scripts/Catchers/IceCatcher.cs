using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceCatcher : MonoBehaviour {
    public Text iceCountText;
    public IcePhase icePhase;

    private int iceCount = 0;
    private int IceCount {
        get {
            return iceCount;
        }

        set {
            iceCount = value;
            Globals.iceCount = IceCount;
            iceCountText.text = $"Ice Count: {IceCount}";
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
