using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log($"mouse position: {mousePosition}");
        Debug.Log($"input mouse position: {Input.mousePosition}");

        transform.position = new Vector3(
            mousePosition.x,
            transform.position.y,
            transform.position.z
        );
    }
}
