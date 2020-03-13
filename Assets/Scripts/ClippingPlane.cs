using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingPlane : MonoBehaviour {
    public Material material;

    private Plane plane;
    private Vector4 planeRepresentation;

    private void Awake() {
        plane = new Plane();
        planeRepresentation = new Vector4();
    }

    private void Update() {
        plane.SetNormalAndPosition(transform.up, transform.position);
        planeRepresentation.Set(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);

        // Pass plane data to shader
        material.SetVector("_Plane", planeRepresentation);
    }
}
