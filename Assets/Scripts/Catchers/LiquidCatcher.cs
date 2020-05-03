using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiquidCatcher : MonoBehaviour {
    private LiquidStream currentLiquidStream;
    public ClippingPlane liquidFillClippingPlane;
    public ClippingPlane liquidStreamClippingPlane;
    public Text liquidPercentageText;
    public float liquidFillSpeed = 2.4f;
    public float LiquidFillTopY {
        get {
            return liquidFillClippingPlaneStartY + (liquidPercentage * liquidFillSpeed);
        }
    }

    private float liquidFillClippingPlaneStartY;
    private float liquidStreamClippingPlaneStartY;
    private float liquidPercentage = 0.0f;
    private float LiquidPercentage {
        get {
            return liquidPercentage;
        }

        set {
            liquidPercentage = Mathf.Min(value, 1.0f);

            Globals.liquidPercentage = liquidPercentage;
            liquidPercentageText.text = $"Liquid Percentage: {Globals.FormattedLiquidPercentage}%";
            liquidFillClippingPlane.transform.localPosition = new Vector3(
                liquidFillClippingPlane.transform.localPosition.x,
                LiquidFillTopY,
                liquidFillClippingPlane.transform.localPosition.z
            );
        }
    }

    private CupEffects cupEffects;

    private void Start() {
        cupEffects = GetComponentInParent<CupEffects>();
        liquidFillClippingPlaneStartY = liquidFillClippingPlane.transform.localPosition.y;
        liquidStreamClippingPlaneStartY = liquidStreamClippingPlane.transform.localPosition.y;
    }

    private void Update() {
        float newLiquidStreamClippingPlaneY;

        if (currentLiquidStream == null) {
            // When the liquid stream isn't colliding with the cup, move the
            // clipping plane off-screen so we don't see the stream getting cut
            // off.
            newLiquidStreamClippingPlaneY = -100.0f;
        } else {
            // When the liquid stream is colliding with the cup, move the
            // clipping plane to the bottom of the cup so the stream is cut off
            // right there.
            newLiquidStreamClippingPlaneY = liquidStreamClippingPlaneStartY;

            LiquidPercentage += Time.deltaTime * 0.2f;
        }

        liquidStreamClippingPlane.transform.localPosition = new Vector3(
            liquidStreamClippingPlane.transform.localPosition.x,
            newLiquidStreamClippingPlaneY,
            liquidStreamClippingPlane.transform.localPosition.z
        );
    }

    private void OnTriggerEnter(Collider other) {
        LiquidStream liquidStream = other.GetComponent<LiquidStream>();

        if (liquidStream != null) {
            currentLiquidStream = liquidStream;
            cupEffects.Lower();
        }
    }

    private void OnTriggerExit(Collider other) {
        LiquidStream liquidStream = other.GetComponent<LiquidStream>();

        if (liquidStream != null) {
            currentLiquidStream = null;
            cupEffects.Raise();
        }
    }
}
