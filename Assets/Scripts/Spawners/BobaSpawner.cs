﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaSpawner : MonoBehaviour {
    public GameObject bobaPrefab;
    public float interval = 0.0001f;

    private bool isSpawning = false;
    private float timeSinceLastBoba = 0.0f;
    private float screenSize;

    private void Start() {
        if (Globals.currentOrder.bobaAmount == Order.AddInOption.None) {
            interval = 0.1f;
        } else if (Globals.currentOrder.bobaAmount == Order.AddInOption.Light) {
            interval = 0.01f;
        } else if (Globals.currentOrder.bobaAmount == Order.AddInOption.Regular) {
            interval = 0.0001f;
        } else if (Globals.currentOrder.bobaAmount == Order.AddInOption.Extra) {
            interval = 0.0001f;
        }

        screenSize = Globals.GetScreenSize(Camera.main);
    }

    private void Update() {
        if (isSpawning) {
            timeSinceLastBoba += Time.deltaTime;

            if (timeSinceLastBoba > interval) {
                GameObject boba = Instantiate(bobaPrefab);
                boba.transform.position = GeneratePosition();
                timeSinceLastBoba = 0.0f;
            }
        }
    }

    public void StartSpawning() {
        isSpawning = true;
    }

    public void StopSpawning() {
        isSpawning = false;
    }

    private Vector3 GeneratePosition() {
        //float xPosition = Debugger.Instance.IsOn ? transform.position.x : Random.Range(-screenSize, screenSize);
        float xPosition = screenSize;

        return new Vector3(
            xPosition,
            transform.position.y,
            transform.position.z
        );
    }
}
