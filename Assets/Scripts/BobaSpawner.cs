using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaSpawner : MonoBehaviour {
    public GameObject bobaPrefab;
    public float interval = 1.0f;

    float timeSinceLastBoba = 0.0f;

    void Start() {
        for (int i = 0; i < 1; i++) {
            Instantiate(bobaPrefab);
        }
    }

    void Update() {
        timeSinceLastBoba += Time.deltaTime;

        if (timeSinceLastBoba > interval) {
            Instantiate(bobaPrefab);
            timeSinceLastBoba = 0.0f;
        }
    }
}
