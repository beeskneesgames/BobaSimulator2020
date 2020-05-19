using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiquidCatcher : MonoBehaviour {
    public GameObject cupBottom;
    public ClippingPlane liquidFillClippingPlane;
    public ClippingPlane liquidStreamClippingPlane;
    public Text liquidPercentageText;
    public float liquidFillSpeed = 2.4f;
    public float LiquidFillTopY {
        get {
            return liquidFillClippingPlaneStartY + (Globals.TotalLiquidPercentage * liquidFillSpeed);
        }
    }

    private LiquidStream currentLiquidStream;
    private LiquidStream CurrentLiquidStream {
        get {
            return currentLiquidStream;
        }

        set {
            LiquidStream oldLiquidStream = currentLiquidStream;
            currentLiquidStream = value;

            if (oldLiquidStream == null && currentLiquidStream != null) {
                // If we just started catching a liquid stream, notify it of the
                // start.
                currentLiquidStream.StartBeingCaught(this);
            } else if (oldLiquidStream != null && currentLiquidStream == null) {
                // If we just stopped catching a liquid stream, notify it of the
                // stop.
                oldLiquidStream.StopBeingCaught(this);
            }
        }
    }
    private float liquidFillClippingPlaneStartY;

    private CupEffects cupEffects;

    private void Start() {
        cupEffects = GetComponentInParent<CupEffects>();
        liquidFillClippingPlaneStartY = liquidFillClippingPlane.transform.localPosition.y;
    }

    private void Update() {
        if (CurrentLiquidStream != null && CurrentLiquidStream.IsShown) {
            Globals.AddLiquid(CurrentLiquidStream.CurrentFlavor, Time.deltaTime * 0.2f);
            UpdateLiquidDisplay();
        }
    }

    private void OnTriggerEnter(Collider other) {
        LiquidStream liquidStream = other.GetComponent<LiquidStream>();

        if (liquidStream != null) {
            CurrentLiquidStream = liquidStream;

            if (liquidStream.IsShown) {
                cupEffects.Lower();
                AudioManager.Instance.StopLiquid();
                AudioManager.Instance.PlayCupLiquid();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        LiquidStream liquidStream = other.GetComponent<LiquidStream>();

        if (liquidStream != null) {
            CurrentLiquidStream = null;

            if (liquidStream.IsShown) {
                cupEffects.Raise();
                AudioManager.Instance.StopCupLiquid();
                AudioManager.Instance.PlayLiquid();
            }
        }
    }

    private void UpdateLiquidDisplay() {
        liquidPercentageText.text = $"Liquid Percentage: {Globals.FormattedLiquidPercentage}%";
        liquidFillClippingPlane.transform.localPosition = new Vector3(
            liquidFillClippingPlane.transform.localPosition.x,
            LiquidFillTopY,
            liquidFillClippingPlane.transform.localPosition.z
        );
    }
}
