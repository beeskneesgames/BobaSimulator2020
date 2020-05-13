using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba : MonoBehaviour {
    private CupEffects cupEffects;
    private bool manuallyFalling;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private float currentTimeFalling = 0.0f;
    private float maxTimeFalling;

    private void Update() {
        if (manuallyFalling) {
            currentTimeFalling += Time.deltaTime;
            float fractionOfJourney = currentTimeFalling / maxTimeFalling;

            transform.localPosition = Vector3.Lerp(
                startingPosition,
                targetPosition,
                fractionOfJourney
            );

            if (fractionOfJourney >= 1.0f) {
                manuallyFalling = false;
                cupEffects.Bounce();

                AudioManager.Instance.PlayBoba();
            }
        } else if (transform.position.y <= -100.0f) {
            Destroy(gameObject);
        }
    }

    public void FallIntoCup(CupController cup) {
        cupEffects = cup.GetComponent<CupEffects>();
        BobaPlacer bobaPlacer = cup.GetComponentInChildren<BobaPlacer>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float velocity = rigidbody.velocity.magnitude;

        manuallyFalling = true;
        transform.parent = bobaPlacer.transform;

        startingPosition = transform.localPosition;
        targetPosition = bobaPlacer.PopPosition(startingPosition);
        maxTimeFalling = Vector3.Distance(startingPosition, targetPosition) / velocity;

        GetComponent<Collider>().enabled = false;
        Destroy(rigidbody);
    }
}
