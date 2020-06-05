using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPlacer : MonoBehaviour {
    public GameObject bobaPrefab;

    // Get the Y-position of the current top layer of boba, so the IcePlacer can
    // start on the correct layer.
    public float TopY {
        get {
            return GenerateYPosition(layerIndex);
        }
    }

    private float bobaSize = 0.18f;
    private int layerIndex = 0;
    private Vector3 defaultPosition;

    private readonly List<List<float>> xLayers = new List<List<float>> {
        new List<float> { -0.05f,  0.5f,  1.0f,  1.6f,  2.0f,  3.0f,  3.8f,  4.0f },
        new List<float> { -0.2f,   0.0f,  0.3f,  1.3f,  2.0f,  2.3f,  3.3f,  3.0f,  4.2f  },
        new List<float> { -0.25f,  0.0f,  0.35f, 1.35f, 1.9f,  2.35f, 3.35f, 4.0f,  4.25f },
        new List<float> { -0.3f,   0.4f,  1.4f,  2.0f,  2.4f,  2.8f,  3.4f,  4.0f,  4.3f  },
        new List<float> { -0.35f,  0.0f,  0.6f,  1.4f,  2.0f,  2.4f,  3.0f,  3.4f,  4.0f,  4.35f },
        new List<float> { -0.4f,   0.0f,  0.7f,  1.5f,  2.5f,  3.0f,  3.7f,  4.2f,  4.4f  },
        new List<float> { -0.45f,  0.0f,  0.8f,  1.6f,  2.6f,  3.0f,  3.8f,  4.1f,  4.45f },
        new List<float> { -0.5f,  -0.1f,  0.6f,  1.3f,  1.9f,  2.0f,  2.8f,  3.6f,  4.3f,  4.5f  },
        new List<float> { -0.55f, -0.2f,  0.7f,  1.2f,  1.8f,  2.1f,  2.6f,  3.4f,  4.2f,  4.55f },
        new List<float> { -0.6f,  -0.3f,  0.5f,  1.0f,  1.8f,  2.2f,  2.5f,  3.4f,  4.2f,  4.6f  },
        new List<float> { -0.65f, -0.3f,  0.4f,  1.1f,  1.9f,  2.3f,  2.6f,  3.5f,  4.3f,  4.65f },
        new List<float> { -0.7f,  -0.4f,  0.3f,  1.1f,  1.7f,  2.2f,  2.7f,  3.5f,  4.4f,  4.7f  },
        new List<float> { -0.75f, -0.5f,  0.4f,  1.2f,  1.6f,  2.3f,  2.8f,  3.6f,  4.5f,  4.75f },
        new List<float> { -0.8f,  -0.7f,  0.2f,  1.0f,  1.5f,  2.1f,  2.6f,  3.4f,  4.3f,  4.8f  },
        new List<float> { -0.85f, -0.6f,  0.3f,  0.9f,  1.4f,  2.0f,  2.8f,  3.2f,  4.1f,  4.85f },
        new List<float> { -0.9f,  -0.7f,  0.2f,  1.0f,  1.5f,  2.1f,  2.6f,  3.4f,  4.3f,  4.9f  },
        new List<float> { -0.95f, -0.5f,  0.1f,  0.8f,  1.3f,  2.0f,  2.8f,  3.2f,  4.1f,  4.95f },
    };

    private List<List<Vector3>> positionLayers;

    private void Awake() {
        // Set up the boba positions
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

    public bool HasPositions() {
        return layerIndex < positionLayers.Count;
    }

    public Vector3 PopPosition(Vector3 startingPosition) {
        // If we run out of positions before the phase is over while boba is still falling,
        // return a previous position to avoid errors.
        if (!HasPositions()) {
            return defaultPosition;
        }

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
        float xPosition = normalizedXPosition * bobaSize;
        float yPosition = GenerateYPosition(layerIndex);
        float zPosition;

        // Hide 10% of the boba when the liquid fills the cup.
        if (Random.value > 0.9f) {
            zPosition = 0.1f;
        } else {
            zPosition = 0.0f;
        }

        // Slightly randomize y/z positions for visual variation.
        return new Vector3(
            xPosition,
            Random.Range(yPosition - 0.04f, yPosition + 0.04f),
            zPosition
        );
    }

    private float GenerateYPosition(int layerIndex) {
        return layerIndex * bobaSize * 0.85f;
    }
}
