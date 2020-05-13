using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {
    private CupEffects cupEffects;
    private bool manuallyFalling;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private float currentTimeFalling = 0.0f;
    private float maxTimeFalling;
    private IcePlacer icePlacer;

    private void Update() {
        if (manuallyFalling) {
            currentTimeFalling += Time.deltaTime;
            float fractionOfJourney = currentTimeFalling / maxTimeFalling;

            if (fractionOfJourney < 1.0f) {
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    Mathf.Lerp(startingPosition.y, targetPosition.y, fractionOfJourney),
                    transform.localPosition.z
                );
            } else {
                manuallyFalling = false;
                transform.localPosition = targetPosition;

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
        icePlacer = cup.GetComponentInChildren<IcePlacer>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float velocity = Mathf.Abs(rigidbody.velocity.y);

        manuallyFalling = true;
        transform.parent = icePlacer.transform;

        startingPosition = transform.localPosition;
        targetPosition = icePlacer.PopPosition(startingPosition);
        maxTimeFalling = (startingPosition.y - targetPosition.y) / velocity;

        GetComponent<Collider>().enabled = false;
        Destroy(rigidbody);
    }
}
