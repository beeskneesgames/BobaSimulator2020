using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacer : MonoBehaviour {
    public GameObject icePrefab;
    public BobaPlacer bobaPlacer;

    float iceSize = 0.5f;
    int layerIndex = 0;
    bool layerIndexCalculated = false;
    Vector3 defaultPosition;

    List<List<float>> icePositions = new List<List<float>> {
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
        float zPosition = 0.075f;

        return new Vector3(xPosition, yPosition, zPosition);
    }
}
