using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour {
    public LiquidStream liquidStreamPrefab;

    private void Start() {
        Instantiate(liquidStreamPrefab);
    }
}
