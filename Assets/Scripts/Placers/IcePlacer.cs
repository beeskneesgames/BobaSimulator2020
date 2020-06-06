using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacer : MonoBehaviour {
    public GameObject icePrefab;
    public BobaPlacer bobaPlacer;
    public ClippingPlane liquidFillClippingPlane;
    public GameObject icePlacerCollider;

    private float iceSize = 0.5f;
    private int layerIndex = 0;
    private bool layerIndexCalculated = false;
    private Vector3 defaultPosition;

    private bool iceFloating = false;
    private float liquidFillClippingPlaneLastY;

    private readonly List<List<float>> xLayers = new List<List<float>> {
        new List<float> {  0.2f,  0.85f },
        new List<float> { -0.05f, 0.9f  },
        new List<float> { -0.1f,  0.55f, 1.18f },
        new List<float> { -0.15f, 0.5f,  1.13f },
        new List<float> { -0.2f,  0.45f, 1.12f },
        new List<float> { -0.25f, 0.4f,  1.07f }
    };

    private List<List<Vector3>> positionLayers;
    private List<List<Ice>> caughtIce = new List<List<Ice>>();

    private void Awake() {
        // Set up the ice positions
        positionLayers = new List<List<Vector3>>(xLayers.Count);

        for (int i = 0; i < xLayers.Count; i++) {
            List<float> xLayer = xLayers[i];
            List<Vector3> positionLayer = new List<Vector3>(xLayer.Count);

            foreach (float x in xLayer) {
                positionLayer.Add(GeneratePosition(x, i));
            }

            positionLayers.Add(positionLayer);
        }

        List<Vector3> lastLayer = positionLayers[positionLayers.Count - 1];
        defaultPosition = lastLayer[lastLayer.Count - 1];
    }

    private void Start() {
        liquidFillClippingPlaneLastY = liquidFillClippingPlane.transform.localPosition.y;

        for (int i = 0; i < xLayers.Count; i++) {
            caughtIce.Add(new List<Ice>());
        }
    }

    private void Update() {
        if (iceFloating) {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y + (liquidFillClippingPlane.transform.localPosition.y - liquidFillClippingPlaneLastY),
                transform.localPosition.z
            );

            liquidFillClippingPlaneLastY = liquidFillClippingPlane.transform.localPosition.y;
        }
    }

    public List<Ice> TopIceLayer {
        get {
            for (int i = caughtIce.Count - 1; i >= 0; i--) {
                if (caughtIce[i].Count > 0) {
                    return caughtIce[i];
                }
            }

            return caughtIce[0];
        }
    }

    public void IcePlaced(Ice ice) {
        // Put the ice placer collider on the same level as the last placed ice
        // so the liquid will hit it there.
        icePlacerCollider.transform.localPosition = new Vector3(
            icePlacerCollider.transform.localPosition.x,
            // Add iceSize so we don't start floating until we're most of the
            // way up the ice.
            ice.transform.localPosition.y + iceSize * 0.85f,
            icePlacerCollider.transform.localPosition.z
        );
    }

    public void StartFloating() {
        iceFloating = true;
        liquidFillClippingPlaneLastY = liquidFillClippingPlane.transform.localPosition.y;
    }

    public bool HasPositions() {
        return layerIndex < xLayers.Count;
    }

    public Vector3 PopPosition(Vector3 startingPosition, Ice ice) {
        // If we run out of positions before the phase is over while ice is still falling,
        // return a previous position to avoid errors.
        if (!HasPositions()) {
            return defaultPosition;
        }

        if (!layerIndexCalculated) {
            layerIndex = Mathf.RoundToInt(bobaPlacer.TopY / iceSize);
            layerIndexCalculated = true;
        }

        caughtIce[layerIndex].Add(ice);

        int closestPositionIndex = 0;
        Vector3 closestPosition = positionLayers[layerIndex][closestPositionIndex];
        float closestDistance = Vector3.Distance(startingPosition, closestPosition);

        // Start at 1, since we started with position 0 above.
        for (int i = 1; i < positionLayers[layerIndex].Count; i++) {
            Vector3 potentialPosition = positionLayers[layerIndex][i];
            float distance = Vector3.Distance(startingPosition, potentialPosition);

            if (distance < closestDistance) {
                closestDistance = distance;
                closestPosition = potentialPosition;
                closestPositionIndex = i;
            }
        }

        positionLayers[layerIndex].RemoveAt(closestPositionIndex);

        if (positionLayers[layerIndex].Count == 0) {
            layerIndex++;
        }

        return closestPosition;
    }

    private Vector3 GeneratePosition(float normalizedXPosition, int layerIndex) {
        float xPosition = normalizedXPosition * iceSize;
        float yPosition = layerIndex * iceSize * 0.75f;
        float zPosition = 0.05f;

        // Slightly randomize y/z positions for visual variation.
        return new Vector3(
            xPosition,
            Random.Range(yPosition - 0.05f, yPosition + 0.05f),
            Random.Range(zPosition - 0.025f, zPosition + 0.025f)
        );
    }
}
