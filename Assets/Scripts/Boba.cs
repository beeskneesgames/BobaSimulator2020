using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba : MonoBehaviour {
    public void FallIntoCup() {
        Debug.Log("Here I go!");
        // Reparent into cup
        // Delete rigidbody
        // Manually fall down (and inward sometimes) by updating position until
        // it hits the bottom collider.
    }
}
