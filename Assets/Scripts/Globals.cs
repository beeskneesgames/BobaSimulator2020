using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals {
    public static int bobaCount = 0;
    public static float liquidPercentage;

    public static float GetScreenSize(Camera camera) {
        return camera.orthographicSize * 1.5f;
    }

    public static string FormattedLiquidPercentage() {
        return (Mathf.Min(liquidPercentage, 1.0f) * 100.0f).ToString("N0");
    }
}
