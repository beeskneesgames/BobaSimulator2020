using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPlacer : MonoBehaviour {
    public GameObject bobaPrefab;

    float bobaSize = 0.18f;
    int layerIndex = 0;
    Vector3 defaultPosition;

    List<List<float>> bobaPositions = new List<List<float>> {
        new List<float> {  0.0f, 1.0f, 2.0f, 3.0f, 4.0f },
        new List<float> { -0.2f, 0.8f, 1.8f, 2.8f, 3.8f },
        new List<float> { -0.4f, 0.6f, 1.6f, 2.6f, 3.6f },
        new List<float> {  0.4f, 1.4f, 2.4f, 3.4f, 4.4f },
    };

    private void Awake() {
        List<float> lastLayer = bobaPositions[bobaPositions.Count - 1];
        defaultPosition = GeneratePosition(lastLayer[lastLayer.Count - 1], bobaPositions.Count - 1);
    }

    public bool HasPositions() {
        return layerIndex < bobaPositions.Count;
    }

    public Vector3 PopPosition() {
        // If we run out of positions before the phase is over while boba is still falling,
        // return a previous position to avoid errors.
        if (!HasPositions()) {
            return defaultPosition;
        }

        int positionIndex = Random.Range(0, bobaPositions[layerIndex].Count);
        Vector3 position = GeneratePosition(bobaPositions[layerIndex][positionIndex], layerIndex);

        bobaPositions[layerIndex].RemoveAt(positionIndex);

        if (bobaPositions[layerIndex].Count == 0) {
            layerIndex++;
        }

        return position;
    }

    private Vector3 GeneratePosition(float normalizedXPosition, int layerIndex) {
        float xPosition = normalizedXPosition * bobaSize;
        float yPosition = layerIndex * bobaSize * 0.85f;
        float zPosition = 0.0f;

        return new Vector3(xPosition, yPosition, zPosition);
    }
}
