using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupEffects : MonoBehaviour {
    enum BounceDirection {
        Up,
        Down
    }

    public float bounceDistance = 0.075f;
    public float bounceSpeed = 2.5f;

    private bool isBouncing;
    private BounceDirection bounceDirection;
    private float startingY;
    private float lowestY;

    private void Update() {
        if (isBouncing) {
            float newY;
            float yOffset = Time.deltaTime * bounceSpeed;

            if (bounceDirection == BounceDirection.Down) {
                newY = transform.position.y - yOffset;

                if (newY < lowestY) {
                    bounceDirection = BounceDirection.Up;
                }
            } else {
                newY = transform.position.y + yOffset;

                if (newY >= startingY) {
                    newY = startingY;
                    isBouncing = false;
                }
            }

            transform.position = new Vector3(
                transform.position.x,
                newY,
                transform.position.z
            );
        }
    }

    public void Bounce() {
        if (!isBouncing) {
            isBouncing = true;
            bounceDirection = BounceDirection.Down;
            startingY = transform.position.y;
            lowestY = startingY - bounceDistance;
        }
    }
}
