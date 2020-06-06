using UnityEngine;

public class LiquidCatcher : MonoBehaviour {
    public GameObject cupBottom;
    public ClippingPlane liquidFillClippingPlane;
    public ClippingPlane liquidStreamClippingPlane;
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
    private readonly float maxFlavorTime = 2.0f;
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
                // If we're in here, we caught a new flavor/color on this frame.
                // 
                // If this is the first color (color A) hitting the cup, the
                // fill should just show A.
                // To keep the code simpler, we lerp from A to A.
                //
                // If this is the second color (color B) hitting the cup, start
                // lerping from A to B.
                // After N seconds, we should be entirely lerped to B. Until then,
                // we're showing a mix between A and B (let's call this color AB).
                //
                // If this is the third color (color C) hitting the cup and
                // we're currently showing AB, start lerping from AB to C.
                // Again, after N seconds, we should be entirely lerped to C.
                targetColor = Order.FlavorColors[CurrentLiquidStream.CurrentFlavor];

                if (currentFlavor == Order.Flavor.NotSet) {
                    previousColor = targetColor;
                } else {
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
        liquidFillClippingPlane.transform.localPosition = new Vector3(
            liquidFillClippingPlane.transform.localPosition.x,
            LiquidFillTopY,
            liquidFillClippingPlane.transform.localPosition.z
        );
    }
}
