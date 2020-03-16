using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacer : MonoBehaviour {
    public GameObject icePrefab;
    public BobaPlacer bobaPlacer;
    public ClippingPlane liquidFillClippingPlane;
    public GameObject liquidFillCollider;

    private float iceSize = 0.5f;
    private int layerIndex = 0;
    private bool layerIndexCalculated = false;
    private Vector3 defaultPosition;

    private bool iceFloating = false;
    private float liquidFillClippingPlaneLastY;

    private List<List<float>> icePositions = new List<List<float>> {
        new List<float> {  0.2f,  0.85f },
        new List<float> { -0.05f, 0.9f  },
        new List<float> { -0.1f,  0.55f, 1.18f },
        new List<float> { -0.15f, 0.5f,  1.13f },
        new List<float> { -0.2f,  0.45f, 1.12f },
        new List<float> { -0.25f, 0.4f,  1.07f }
    };

    private void Awake() {
        List<float> lastLayer = icePositions[icePositions.Count - 1];
        defaultPosition = GeneratePosition(lastLayer[lastLayer.Count - 1], icePositions.Count - 1);
    }

    private void Start() {
        liquidFillClippingPlaneLastY = liquidFillClippingPlane.transform.localPosition.y;
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

    public void IcePlaced(Ice ice) {
        // Put the liquid fill collider on the same level as the last placed ice
        // so the liquid will hit it there.
        liquidFillCollider.transform.localPosition = new Vector3(
            liquidFillCollider.transform.localPosition.x,
            // Add iceSize so we don't start floating until we're most of the
            // way up the ice.
            ice.transform.localPosition.y + iceSize * 0.15f,
            liquidFillCollider.transform.localPosition.z
        );
    }

    public void StartFloating() {
        iceFloating = true;
        liquidFillClippingPlaneLastY = liquidFillClippingPlane.transform.localPosition.y;
    }

    public bool HasPositions() {
        return layerIndex < icePositions.Count;
    }

    public Vector3 PopPosition() {
        // If we run out of positions before the phase is over while ice is still falling,
        // return a previous position to avoid errors.
        if (!HasPositions()) {
            return defaultPosition;
        }

        if (!layerIndexCalculated) {
            layerIndex = Mathf.RoundToInt(bobaPlacer.TopY / iceSize);
            layerIndexCalculated = true;
        }

        int positionIndex = Random.Range(0, icePositions[layerIndex].Count);
        Vector3 position = GeneratePosition(icePositions[layerIndex][positionIndex], layerIndex);

        icePositions[layerIndex].RemoveAt(positionIndex);

        if (icePositions[layerIndex].Count == 0) {
            layerIndex++;
        }

        return position;
    }

    private Vector3 GeneratePosition(float normalizedXPosition, int layerIndex) {
        float xPosition = normalizedXPosition * iceSize;
        float yPosition = layerIndex * iceSize * 0.75f;
        float zPosition = 0.05f;

        return new Vector3(
            xPosition,
            Random.Range(yPosition - 0.05f, yPosition + 0.05f),
            Random.Range(zPosition - 0.025f, zPosition + 0.025f)
        );
    }
}
