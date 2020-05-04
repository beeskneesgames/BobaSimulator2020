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

    public LiquidCatcher currentCatcher;
    public ClippingPlane clippingPlane;
    public Transition CurrentTransition {
        get;
        private set;
    }

    private void Awake() {
        CurrentTransition = Transition.None;
    }

    private void Update() {
        UpdateClippingPlaneY();
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

    public void StartBeingCaught(LiquidCatcher catcher) {
        currentCatcher = catcher;
    }

    public void StopBeingCaught(LiquidCatcher catcher) {
        currentCatcher = null;
    }

    // TODO: Fix clipping plane bug
    //
    // Current problem: This method immediately resets the clipping plane
    // to y=-100 even though it should be giving up control to the stream (since
    // it's transitioning at the beginning).
    //
    // Old Reason: We're in the *catcher* here. That means currentLiquidStream is
    // only set during a collision. When it's not set (because it's not hitting
    // the cup), we have no way of knowing if it's transitioning or not, so we assume
    // it's not and show the whole stream.
    //
    // Possible solution: Move some control stuff around so that the stream
    // always controls its clipping plane.
    private void UpdateClippingPlaneY() {
        if (currentCatcher == null) {
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
                currentCatcher.cupBottom.transform.position.y,
                clippingPlane.transform.position.z
            );
        }

    }
}
