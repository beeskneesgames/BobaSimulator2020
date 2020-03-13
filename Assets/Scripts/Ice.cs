using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {
    private bool manuallyFalling;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private float timeFalling = 0.0f;

    private void Update() {
        if (manuallyFalling) {
            float fractionOfJourney = timeFalling / 0.1f;

            transform.localPosition = Vector3.Lerp(
                startingPosition,
                targetPosition,
                fractionOfJourney
            );
            timeFalling += Time.deltaTime;

            if (Mathf.Approximately(fractionOfJourney, 1.0f)) {
                manuallyFalling = false;
            }
        } else if (transform.position.y <= -100.0f) {
            Destroy(gameObject);
        }
    }

    public void FallIntoCup(CupController cup) {
        IcePlacer icePlacer = cup.GetComponentInChildren<IcePlacer>();

        transform.parent = icePlacer.transform;

        manuallyFalling = true;
        startingPosition = transform.localPosition;
        targetPosition = icePlacer.PopPosition();

        GetComponent<Collider>().enabled = false;
        Destroy(GetComponent<Rigidbody>());
    }
}
