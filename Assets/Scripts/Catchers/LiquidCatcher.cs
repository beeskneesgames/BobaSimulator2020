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
    public LiquidPhase liquidPhase;
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

    private Order.Flavor currentFlavor = Order.Flavor.NotSet;
    private float currentFlavorTime = 0.0f;
    private readonly float maxFlavorTime = 3.0f;
    private Color previousColor;
    private Color targetColor;

    private CupEffects cupEffects;

    private void Start() {
        cupEffects = GetComponentInParent<CupEffects>();
        liquidFillClippingPlaneStartY = liquidFillClippingPlane.transform.localPosition.y;
    }

    private void Update() {
        if (CurrentLiquidStream != null && CurrentLiquidStream.IsShown) {
            if (currentFlavor != CurrentLiquidStream.CurrentFlavor) {
                targetColor = Order.FlavorColors[CurrentLiquidStream.CurrentFlavor];

                if (currentFlavor == Order.Flavor.NotSet) {
                    // When color A hits the cup, the fill should just be A.
                    // To keep the code simpler, we lerp from A to A.
                    previousColor = targetColor;
                } else {
                    // When color B hits the cup, start lerping from A to B.
                    // After 3 seconds, we should be entirely lerped to B. Until then,
                    // we're showing a mix between A and B (let's call this color AB).
                    //
                    // If color C hits the cup while we're showing AB, start lerping
                    // from AB to C.
                    // Again, after 3 seconds, we should be entirely lerped to C.
                    previousColor = Globals.liquidFillColor;
                }

                currentFlavorTime = 0.0f;
                currentFlavor = CurrentLiquidStream.CurrentFlavor;
            }

            currentFlavorTime += Time.deltaTime;
            Globals.liquidFillColor = Color.Lerp(previousColor, targetColor, currentFlavorTime / maxFlavorTime);

            Globals.AddLiquid(CurrentLiquidStream.CurrentFlavor, Time.deltaTime * 0.1f);
            UpdateLiquidDisplay();

            if (liquidPhase.ShouldEndEarly()) {
                liquidPhase.EndPhaseEarly();
            }
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
