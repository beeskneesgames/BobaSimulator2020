using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawner : MonoBehaviour {
    public GameObject icePrefab;
    public float interval;
    private bool isSpawning = false;
    private float timeSinceLastIce = 0.0f;
    private float screenSize;

    private void Start() {
        screenSize = Globals.GetScreenSize(Camera.main);
    }

    private void Update() {
        if (isSpawning) {
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
    }

    public void StartSpawning() {
        if (Globals.currentOrder.iceAmount == Order.AddInOption.None ||
            Globals.currentOrder.iceAmount == Order.AddInOption.Light) {
            interval = 0.5f;
        } else if (Globals.currentOrder.iceAmount == Order.AddInOption.Regular) {
            interval = 0.25f;
        } else {
            interval = 0.75f;
        }

        isSpawning = true;
    }

    public void StopSpawning() {
        isSpawning = false;
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
