using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingPlane : MonoBehaviour {
    public Material material;

    private void Update() {
        material.SetVector("_PlanePosition", transform.position);
        material.SetVector("_PlaneNormal", transform.up);
    }
}
