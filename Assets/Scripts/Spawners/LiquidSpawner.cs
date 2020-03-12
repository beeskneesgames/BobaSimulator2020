using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour {
    public float interval = 1.0f;
    public LiquidStream liquidStreamPrefab;

    private LiquidStream liquidStream;
    private float timeSinceLastStream = 0.0f;
    private float screenSize;

    private void Start() {
        screenSize = Globals.GetScreenSize(Camera.main);
        liquidStream = Instantiate(liquidStreamPrefab);
        liquidStream.transform.position = GeneratePosition();
    }

    private void Update() {
        timeSinceLastStream += Time.deltaTime;

        if (timeSinceLastStream > interval) {
            liquidStream.transform.position = GeneratePosition();
            timeSinceLastStream = 0.0f;
        }
    }

    private Vector3 GeneratePosition() {
        float xPosition = Debugger.Instance.IsOn ? 0.0f : Random.Range(-screenSize, screenSize);
        return new Vector3(xPosition, 0.0f, 0.5f);
    }

    private void OnDisable() {
        if (liquidStream != null) {
            Destroy(liquidStream.gameObject);
        }
    }
}
