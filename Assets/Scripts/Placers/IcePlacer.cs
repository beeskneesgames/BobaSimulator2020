using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacer : MonoBehaviour {
    public GameObject icePrefab;

    float iceSize = 1.0f;
    int layerIndex = 0;
    Vector3 defaultPosition;

    List<List<float>> icePositions = new List<List<float>> {
        new List<float> { 0.0f, 1.0f, 2.0f },
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
        float zPosition = 0.0f;

        return new Vector3(xPosition, yPosition, zPosition);
    }
}
