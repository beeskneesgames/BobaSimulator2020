using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {
    public bool IsCaught {
        get {
            return manuallyFalling;
        }
    }

    private CupEffects cupEffects;
    private bool manuallyFalling;
    private Vector3 startingPosition;
    private Vector3 targetLocalPosition;
    private Vector3 targetWorldPosition;
    private float currentTimeFalling = 0.0f;
    private float maxTimeFalling;
    private IcePlacer icePlacer;
    private bool keepAfterLanding;

    private void Update() {
        if (manuallyFalling) {
            currentTimeFalling += Time.deltaTime;
            float fractionOfJourney = currentTimeFalling / maxTimeFalling;

            if (maxTimeFalling > 0.0f && fractionOfJourney < 1.0f) {
                Vector3 lastPosition = transform.position;
                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Lerp(startingPosition.y, targetWorldPosition.y, fractionOfJourney),
                    targetWorldPosition.z
                );
            } else {
                manuallyFalling = false;

                if (keepAfterLanding) {
                    transform.parent = icePlacer.transform;
                    transform.localPosition = targetLocalPosition;

                    // Let the ice placer know that we're done being placed.
                    icePlacer.IcePlaced(this);
                } else {
                    Destroy(gameObject);
                }

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

        keepAfterLanding = icePlacer.HasPositions();
        startingPosition = transform.localPosition;
        targetLocalPosition = icePlacer.PopPosition(startingPosition, this);
        targetWorldPosition = icePlacer.transform.TransformPoint(targetLocalPosition);
        maxTimeFalling = (startingPosition.y - targetWorldPosition.y) / velocity;

        Destroy(GetComponent<Collider>());
        Destroy(rigidbody);
    }
}
