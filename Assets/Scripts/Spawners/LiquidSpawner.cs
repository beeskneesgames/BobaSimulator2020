﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour {
    public float interval = 2.0f;
    public LiquidStream liquidStreamPrefab;

    private bool isSpawning = false;
    private LiquidStream liquidStream;
    private float timeSinceLastStream = 0.0f;
    private float screenSize;

    private void Start() {
        screenSize = Globals.GetScreenSize(Camera.main);
        liquidStream = Instantiate(liquidStreamPrefab);
        liquidStream.transform.position = GeneratePosition();
        liquidStream.Hide();
    }

    private void Update() {
        if (isSpawning) {
            timeSinceLastStream += Time.deltaTime;

            if (timeSinceLastStream > interval) {
                liquidStream.MoveTo(GeneratePosition());
                timeSinceLastStream = 0.0f;
            }
        }
    }

    private Vector3 GeneratePosition() {
        float xPosition = Debugger.Instance.IsOn ? 0.0f : Random.Range(-screenSize, screenSize);

        // Add 0.2 to the z-position here so the liquid stream appears to be
        // going into the liquid fill. Without this, part of the stream will
        // appear "in front of" the fill, breaking the illusion of it filling
        // the cup.
        return new Vector3(
            xPosition,
            0.0f,
            transform.position.z + 0.2f
        );
    }

    public void StartSpawning() {
        isSpawning = true;
        liquidStream.Show();
    }

    public void StopSpawning() {
        isSpawning = false;
        liquidStream.Hide();
    }
}
