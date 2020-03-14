using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacer : MonoBehaviour {
    public GameObject icePrefab;
    public BobaPlacer bobaPlacer;
    public ClippingPlane liquidFillClippingPlane;

    private float iceSize = 0.5f;
    private int layerIndex = 0;
    private bool layerIndexCalculated = false;
    private bool iceFloating = false;
    private float liquidFillClippingPlaneStartY;
    private Vector3 defaultPosition;
    private float startY;
    private float lastPoppedY;

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
        startY = transform.localPosition.y;
    }

    private void Start() {
        liquidFillClippingPlaneStartY = liquidFillClippingPlane.transform.localPosition.y;
    }

    private void Update() {
        // TODO: Fix this whole mess.
        float liquidY = liquidFillClippingPlane.transform.localPosition.y;

        if (!iceFloating) {
            float topLayerY = lastPoppedY;

            if (liquidY - liquidFillClippingPlaneStartY > topLayerY) {
                iceFloating = true;
            }
        }

        if (iceFloating) {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                liquidY - lastPoppedY,
                transform.localPosition.z
            );
        }
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

        // TODO: Gross get rid of
        lastPoppedY = GenerateYPosition(layerIndex);

        icePositions[layerIndex].RemoveAt(positionIndex);

        if (icePositions[layerIndex].Count == 0) {
            layerIndex++;
        }

        return position;
    }

    private Vector3 GeneratePosition(float normalizedXPosition, int layerIndex) {
        float xPosition = normalizedXPosition * iceSize;
        float yPosition = GenerateYPosition(layerIndex);
        float zPosition = 0.05f;

        return new Vector3(
            xPosition,
            Random.Range(yPosition - 0.05f, yPosition + 0.05f),
            Random.Range(zPosition - 0.025f, zPosition + 0.025f)
        );
    }

    private float GenerateYPosition(float layerIndex) {
        return layerIndex * iceSize * 0.75f;
    }
}
