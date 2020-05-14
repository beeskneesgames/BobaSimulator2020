using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingPlane : MonoBehaviour {
    public new Renderer renderer;
    private Material material;

    private void Start() {
        material = renderer.material;
    }

    private void Update() {
        material.SetVector("_PlanePosition", transform.position);
        material.SetVector("_PlaneNormal", transform.up);
    }
}
