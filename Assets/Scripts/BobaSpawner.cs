using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaSpawner : MonoBehaviour {
    public GameObject bobaPrefab;

    void Start() {
        Instantiate(bobaPrefab);
    }

    void Update() {

    }
}
