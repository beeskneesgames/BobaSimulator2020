using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiquidCatcher : MonoBehaviour {
    private LiquidStream currentLiquidStream;
    public ClippingPlane clippingPlane;
    public Text liquidPercentageText;

    private float clippingPlaneStartY;
    private float liquidPercentage = 0.0f;
    private float LiquidPercentage {
        get {
            return liquidPercentage;
        }

        set {
            liquidPercentage = Mathf.Min(value, 1.0f);

            Globals.liquidPercentage = liquidPercentage;
            liquidPercentageText.text = $"Liquid Percentage: {Globals.FormattedLiquidPercentage}%";
            clippingPlane.transform.position = new Vector3(
                clippingPlane.transform.position.x,
                clippingPlaneStartY + (liquidPercentage * 2.4f),
                clippingPlane.transform.position.z
            );
        }
    }

    private void Start() {
        clippingPlaneStartY = clippingPlane.transform.position.y;
    }

    private void Update() {
        if (currentLiquidStream != null) {
            LiquidPercentage += Time.deltaTime * 0.2f;
        }
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
