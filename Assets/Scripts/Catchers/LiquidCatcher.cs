using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiquidCatcher : MonoBehaviour
{
    private LiquidStream currentLiquidStream;
    public Text liquidPercentageText;
    private float liquidPercentage = 0.0f;
    private float LiquidPercentage {
        get {
            return liquidPercentage;
        }

        set {
            liquidPercentage = Mathf.Min(value, 1.0f);
            liquidPercentageText.text = $"Liquid Percentage: {LiquidPercentage * 100.0f}%";
        }
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
