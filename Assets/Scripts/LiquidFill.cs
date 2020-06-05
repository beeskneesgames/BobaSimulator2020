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
            renderer.material.SetColor("_BaseColor", Globals.liquidFillColor);
        }
    }
}
