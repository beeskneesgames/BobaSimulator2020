using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawner : MonoBehaviour {
    public GameObject icePrefab;
    public float interval = 0.2f;

    private float timeSinceLastIce = 0.0f;
    private float screenSize;

    private void Start() {
        screenSize = Globals.GetScreenSize(Camera.main);
    }

    private void Update() {
        timeSinceLastIce += Time.deltaTime;

        if (timeSinceLastIce > interval) {
            GameObject ice = Instantiate(icePrefab);
            ice.transform.position = GeneratePosition();
            ice.transform.Rotate(
                Random.Range(0.0f, 360.0f),
                Random.Range(0.0f, 360.0f),
                Random.Range(0.0f, 360.0f),
                Space.Self
            );
            timeSinceLastIce = 0.0f;
        }
    }

    private Vector3 GeneratePosition() {
        float xPosition = Debugger.Instance.IsOn ? transform.position.x : Random.Range(-screenSize, screenSize);
        return new Vector3(
            xPosition,
            transform.position.y,
            transform.position.z
        );
    }
}
