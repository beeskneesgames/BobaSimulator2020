using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba : MonoBehaviour {
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
    private BobaPlacer bobaPlacer;

    private void Update() {
        if (manuallyFalling) {
            currentTimeFalling += Time.deltaTime;
            float fractionOfJourney = currentTimeFalling / maxTimeFalling;

            if (fractionOfJourney < 1.0f) {
                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Lerp(startingPosition.y, targetWorldPosition.y, fractionOfJourney),
                    transform.position.z
                );
            } else {
                manuallyFalling = false;
                transform.parent = bobaPlacer.transform;
                transform.localPosition = targetLocalPosition;
                cupEffects.Bounce();

                AudioManager.Instance.PlayBoba();
            }
        } else if (transform.position.y <= -100.0f) {
            Destroy(gameObject);
        }
    }

    public void FallIntoCup(CupController cup) {
        cupEffects = cup.GetComponent<CupEffects>();
        bobaPlacer = cup.GetComponentInChildren<BobaPlacer>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float velocity = Mathf.Abs(rigidbody.velocity.y);

        manuallyFalling = true;

        startingPosition = transform.position;
        targetLocalPosition = bobaPlacer.PopPosition(startingPosition);
        targetWorldPosition = bobaPlacer.transform.TransformPoint(targetLocalPosition);
        maxTimeFalling = (startingPosition.y - targetWorldPosition.y) / velocity;

        Destroy(GetComponent<Collider>());
        Destroy(rigidbody);
    }
}
