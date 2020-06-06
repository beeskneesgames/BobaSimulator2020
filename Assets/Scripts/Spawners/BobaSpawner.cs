using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaSpawner : MonoBehaviour {
    public GameObject bobaPrefab;
    public float interval = 0.01f;

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
                GameObject boba1 = Instantiate(bobaPrefab);

                boba1.transform.position = GeneratePosition();


                //if (Globals.currentOrder.bobaAmount == Order.AddInOption.Regular ||
                //    Globals.currentOrder.bobaAmount == Order.AddInOption.Extra) {
                //    GameObject boba2 = Instantiate(bobaPrefab);
                //    boba2.transform.position = GeneratePosition();
                //}

                //if (Globals.currentOrder.bobaAmount == Order.AddInOption.Extra) {
                //    GameObject boba3 = Instantiate(bobaPrefab);
                //    boba3.transform.position = GeneratePosition();
                //}

                timeSinceLastBoba = 0.0f;
            }
        }
    }

    public void StartSpawning() {
        if (Globals.currentOrder.bobaAmount == Order.AddInOption.None) {
            interval = 2.0f;
        } else if (Globals.currentOrder.bobaAmount == Order.AddInOption.Light) {
            interval = 1.0f;
        } else {
            interval = 0.05f;
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
