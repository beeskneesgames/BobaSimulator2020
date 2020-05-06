using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidStream : MonoBehaviour {
    public enum Transition {
        In,
        Out,
        None
    }

    public LiquidCatcher liquidCatcher;
    public CupEffects cupEffects;
    public ClippingPlane clippingPlane;
    public Transition CurrentTransition {
        get;
        private set;
    }

    public bool IsShown {
        get;
        private set;
    }

    private void Awake() {
        CurrentTransition = Transition.None;
    }

    private void Update() {
        switch (CurrentTransition) {
            case Transition.In:
                break;
            case Transition.Out:
                break;
            case Transition.None:
                UpdateClippingPlaneY();
                break;
        }
    }

    public void TransitionIn() {
        CurrentTransition = Transition.In;

        // TODO: When the stream transitions in, move the clipping plane up so it cuts
        // off the whole thing. This is because we're going to animate it in.
    }

    public void TransitionOut() {
        CurrentTransition = Transition.Out;
    }

    public bool IsTransitioning() {
        return CurrentTransition != Transition.None;
    }

    public void Show() {
        if (!IsShown) {
            IsShown = true;

            // If we're already colliding with the cup when we start showing
            // the stream, lower the cup, since that's what we normally do at
            // the beginning of the collision (which is what it will be from the
            // player's perspective).
            if (liquidCatcher != null) {
                cupEffects.Lower();
            }
        }
    }

    public void Hide() {
        IsShown = false;
        clippingPlane.transform.localPosition = new Vector3(
            clippingPlane.transform.localPosition.x,
            1.0f,
            clippingPlane.transform.localPosition.z
        );
    }

    public void StartBeingCaught(LiquidCatcher catcher) {
        liquidCatcher = catcher;
        cupEffects = catcher.GetComponentInParent<CupEffects>();
    }

    public void StopBeingCaught(LiquidCatcher catcher) {
        liquidCatcher = null;
        cupEffects = null;
    }

    private void UpdateClippingPlaneY() {
        if (IsShown) {
            if (liquidCatcher == null) {
                // When the liquid stream isn't being caught, move the clipping
                // plane off-screen so we don't see the stream getting cut off.
                clippingPlane.transform.localPosition = new Vector3(
                    clippingPlane.transform.localPosition.x,
                    -100.0f,
                    clippingPlane.transform.localPosition.z
                );
            } else {
                // When the liquid stream is being caught, move the clipping plane
                // to the center of the catcher so the stream is cut off right there.
                //
                // Note: We use position instead of localPosition here because the
                // catcher is parented to the cup rather than the liquid stream.
                clippingPlane.transform.position = new Vector3(
                    clippingPlane.transform.position.x,
                    liquidCatcher.cupBottom.transform.position.y,
                    clippingPlane.transform.position.z
                );
            }
        }

    }
}
