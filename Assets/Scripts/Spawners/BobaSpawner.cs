using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaSpawner : MonoBehaviour {
    public GameObject bobaPrefab;
    public float interval = 0.0001f;

    private bool isSpawning = false;
    private float timeSinceLastBoba = 0.0f;
    private float screenSize;

    private void Start() {
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
