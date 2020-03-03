using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour {
    public LiquidStream liquidStreamPrefab;
    private LiquidStream liquidStream;

    private void Start() {
        liquidStream = Instantiate(liquidStreamPrefab);
    }

    private void OnDisable() {
        if (liquidStream != null) {
            Destroy(liquidStream.gameObject);
        }
    }
}
