﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobaCatcher : MonoBehaviour {
    public Text bobaCountText;
    private int bobaCount = 0;
    private int BobaCount {
        get {
            return bobaCount;
        }

        set {
            bobaCount = value;
            bobaCountText.text = $"Boba Count: {BobaCount}";
        }
    }

    private void OnTriggerEnter(Collider other) {
        Boba boba = other.GetComponent<Boba>();

        if (boba != null) {
            boba.FallIntoCup(GetComponentInParent<CupController>());
            BobaCount++;
        }
    }
}
