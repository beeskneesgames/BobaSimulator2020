using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupEffects : MonoBehaviour {
    enum BounceDirection {
        Up,
        Down
    }

    enum Effect {
        None,
        Bounce,
        Lower,
        Raise
    }

    public float yMoveDistance = 0.075f;
    public float yMoveSpeed = 2.5f;

    private Effect currentEffect = Effect.None;
    private BounceDirection currentBounceDirection;
    private float startingY;
    private float lowestY;

    private void Start() {
        startingY = transform.localPosition.y;
        lowestY = startingY - yMoveDistance;
    }

    private void Update() {
        switch (currentEffect) {
            case Effect.Bounce:
                UpdateBounce();
                break;
            case Effect.Lower:
                UpdateLower();
                break;
            case Effect.Raise:
                UpdateRaise();
                break;
        }
    }

    public void Bounce() {
        if (currentEffect == Effect.None) {
            currentEffect = Effect.Bounce;
            currentBounceDirection = BounceDirection.Down;
        }
    }

    public void Lower() {
        if (currentEffect == Effect.None) {
            currentEffect = Effect.Lower;
        }
    }

    public void Raise() {
        if (currentEffect == Effect.None) {
            currentEffect = Effect.Raise;
        }
    }

    private void UpdateBounce() {
        float newY;
        float yOffset = Time.deltaTime * yMoveSpeed;

        if (currentBounceDirection == BounceDirection.Down) {
            newY = transform.localPosition.y - yOffset;

            if (newY < lowestY) {
                currentBounceDirection = BounceDirection.Up;
            }
        } else {
            newY = transform.localPosition.y + yOffset;

            if (newY >= startingY) {
                newY = startingY;
                currentEffect = Effect.None;
            }
        }

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            newY,
            transform.localPosition.z
        );
    }

    private void UpdateLower() {
        float yOffset = Time.deltaTime * yMoveSpeed;
        float newY = transform.localPosition.y - yOffset;

        if (newY < lowestY) {
            newY = lowestY;
            currentEffect = Effect.None;
        }

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            newY,
            transform.localPosition.z
        );
    }

    private void UpdateRaise() {
        float yOffset = Time.deltaTime * yMoveSpeed;
        float newY = transform.localPosition.y + yOffset;

        if (newY > startingY) {
            newY = startingY;
            currentEffect = Effect.None;
        }

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            newY,
            transform.localPosition.z
        );
    }
}
