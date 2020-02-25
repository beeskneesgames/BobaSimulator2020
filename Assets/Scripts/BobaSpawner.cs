﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaSpawner : MonoBehaviour {
    public GameObject bobaPrefab;
    public float interval = 1.0f;

    float timeSinceLastBoba = 0.0f;

    void Update() {
        timeSinceLastBoba += Time.deltaTime;

        if (timeSinceLastBoba > interval) {
            GameObject boba = Instantiate(bobaPrefab);
            float xPosition = GlobalVariables.isDebug ? transform.position.x : Random.Range(-10.0f, 10.0f);

            boba.transform.position = new Vector3(
                xPosition,
                transform.position.y,
                transform.position.z
            );

            timeSinceLastBoba = 0.0f;
        }
    }
}
