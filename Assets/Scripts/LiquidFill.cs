using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidFill : MonoBehaviour {
    private new Renderer renderer;
    private float percentage;

    private void Awake() {
        renderer = GetComponent<Renderer>();
    }

    private void Update() {
        if (Globals.TotalLiquidPercentage > percentage) {
            percentage = Globals.TotalLiquidPercentage;
            Color mixedColor = new Color(0.0f, 0.0f, 0.0f);

            foreach (KeyValuePair<Order.Flavor, float> pair in Globals.liquidPercentages) {
                mixedColor += Order.FlavorColors[pair.Key] * (pair.Value / percentage);
            }

            renderer.material.SetColor("_BaseColor", mixedColor);
        }

    }
}
