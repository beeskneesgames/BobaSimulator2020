using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPlacer : MonoBehaviour {
    public GameObject bobaPrefab;

    float bobaSize = 0.5f;

    List<List<float>> bobaPositions = new List<List<float>> {
        new List<float> { 0.0f, 1.0f, 2.0f },
        new List<float> { 0.0f, 1.0f, 2.0f },
        new List<float> { 0.0f, 1.0f, 2.0f }
    } ;

    void Start() {
        GameObject boba = Instantiate(bobaPrefab, transform);
        Destroy(boba.GetComponent<Rigidbody>());
    }
}
