using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPlacer : MonoBehaviour {
    public GameObject bobaPrefab;

    float bobaSize = 0.18f;
    int layerIndex = 0;

    List<List<float>> bobaPositions = new List<List<float>> {
        new List<float> {  0.0f, 1.0f, 2.0f, 3.0f, 4.0f },
        new List<float> { -0.2f, 0.8f, 1.8f, 2.8f, 3.8f },
        new List<float> { -0.4f, 0.6f, 1.6f, 2.6f, 3.6f },
        new List<float> {  0.4f, 1.4f, 2.4f, 3.4f, 4.4f },
    } ;

    public Vector3 PopPosition() {
        int positionIndex = Random.Range(0, bobaPositions[layerIndex].Count);
        float xPosition = bobaPositions[layerIndex][positionIndex] * bobaSize;
        float yPosition = (float)layerIndex * bobaSize * 0.85f;
        float zPosition = 0.0f;
        Vector3 position = new Vector3(xPosition, yPosition, zPosition);

        bobaPositions[layerIndex].RemoveAt(positionIndex);

        if (bobaPositions[layerIndex].Count == 0) {
            layerIndex++;
        }

        return position;
    }
}
