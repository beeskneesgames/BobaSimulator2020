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
                liquidFillClippingPlaneStartY + (liquidPercentage * liquidFillSpeed),
                liquidFillClippingPlane.transform.localPosition.z
            );
        }
    }

    private void Start() {
        liquidFillClippingPlaneStartY = liquidFillClippingPlane.transform.localPosition.y;
        liquidStreamClippingPlaneStartY = liquidStreamClippingPlane.transform.localPosition.y;
    }

    private void Update() {
        float newClippingPlaneY;

        if (currentLiquidStream == null) {
            newClippingPlaneY = -100.0f;
        } else {
            newClippingPlaneY = liquidStreamClippingPlaneStartY;
            LiquidPercentage += Time.deltaTime * 0.2f;
        }

        liquidStreamClippingPlane.transform.localPosition = new Vector3(
            liquidStreamClippingPlane.transform.localPosition.x,
            newClippingPlaneY,
            liquidStreamClippingPlane.transform.localPosition.z
        );
    }

    private void OnTriggerEnter(Collider other) {
        LiquidStream liquidStream = other.GetComponent<LiquidStream>();

        if (liquidStream != null) {
            currentLiquidStream = liquidStream;
        }
    }

    private void OnTriggerExit(Collider other) {
        LiquidStream liquidStream = other.GetComponent<LiquidStream>();

        if (liquidStream != null) {
            currentLiquidStream = null;
        }
    }
}
