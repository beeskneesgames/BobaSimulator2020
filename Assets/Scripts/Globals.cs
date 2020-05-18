using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals {
    public static int bobaCount;
    public static int iceCount;
    public static float liquidPercentage;
    public static Order currentOrder;
    public static string FormattedLiquidPercentage {
        get {
            return (Mathf.Min(liquidPercentage, 1.0f) * 100.0f).ToString("N0");
        }
    }

    public static float GetScreenSize(Camera camera) {
        return camera.orthographicSize * 1.5f;
    }
}
