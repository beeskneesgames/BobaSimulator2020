using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {
    private CupEffects cupEffects;
    private bool manuallyFalling;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private float timeFalling = 0.0f;
    private IcePlacer icePlacer;

    private void Update() {
        if (manuallyFalling) {
            float fractionOfJourney = timeFalling / 0.1f;

            transform.localPosition = Vector3.Lerp(
                startingPosition,
                targetPosition,
                fractionOfJourney
            );
            timeFalling += Time.deltaTime;

            if (fractionOfJourney >= 1.0f) {
                manuallyFalling = false;

                // Let the ice placer know that we're done being placed.
                icePlacer.IcePlaced(this);
                cupEffects.Bounce();

                AudioManager.Instance.PlayIce();
            }
        } else if (transform.position.y <= -100.0f) {
            Destroy(gameObject);
        }
    }

    public void FallIntoCup(CupController cup) {
        cupEffects = cup.GetComponent<CupEffects>();
        manuallyFalling = true;
        startingPosition = transform.localPosition;

        GetComponent<Collider>().enabled = false;
        Destroy(GetComponent<Rigidbody>());

        icePlacer = cup.GetComponentInChildren<IcePlacer>();
        transform.parent = icePlacer.transform;
        targetPosition = icePlacer.PopPosition();
    }
}
