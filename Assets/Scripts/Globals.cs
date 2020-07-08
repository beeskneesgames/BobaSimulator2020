using System.Collections.Generic;
using UnityEngine;

public class Globals {
    public static bool isPaused = false;

    public static CupContainer cupContainer;

    public static int bobaCount;
    public static int maxBobaCount = 163;
    public static int orderCount = 0;
    public static int iceCount;
    public static int maxIceCount = 16;

    public static Dictionary<Order.Flavor, float> liquidPercentages = new Dictionary<Order.Flavor, float>();
    public static Order currentOrder;
    public static string FormattedLiquidPercentage {
        get {
            return (Mathf.Min(TotalLiquidPercentage, 1.0f) * 100.0f).ToString("N0");
        }
    }

    public static float TotalLiquidPercentage {
        get;
        private set;
    }

    public static Color liquidFillColor;

    public static void AddLiquid(Order.Flavor flavor, float percentage) {
        // Clamp the percentage so we don't go above 1.
        float remainingLiquidPercentage = 1.0f - TotalLiquidPercentage;
        float clampedPercentage = Mathf.Min(percentage, remainingLiquidPercentage);

        if (liquidPercentages.ContainsKey(flavor)) {
            liquidPercentages[flavor] += clampedPercentage;
        } else {
            liquidPercentages.Add(flavor, clampedPercentage);
        }

        TotalLiquidPercentage += clampedPercentage;
    }

    public static void ResetLiquid() {
        liquidPercentages.Clear();
        TotalLiquidPercentage = 0.0f;
    }

    public static float GetScreenSize(Camera camera) {
        return camera.orthographicSize * 1.5f;
    }
}
