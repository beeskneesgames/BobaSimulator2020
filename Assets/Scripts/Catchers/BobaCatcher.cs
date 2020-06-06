using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobaCatcher : MonoBehaviour {
    public BobaPhase bobaPhase;

    private int bobaCount = 0;
    private int BobaCount {
        get {
            return bobaCount;
        }

        set {
            bobaCount = value;
            Globals.bobaCount = BobaCount;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Boba boba = other.GetComponent<Boba>();

        if (boba != null && !boba.IsCaught) {
            boba.FallIntoCup(GetComponentInParent<CupController>());
            BobaCount++;

            if (bobaPhase.ShouldEndEarly()) {
                bobaPhase.EndPhaseEarly();
            }
        }
    }
}
