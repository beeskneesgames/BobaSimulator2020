using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidStream : MonoBehaviour {
    private const float PourSpeed = 15.0f;

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

    private Vector3 startingTransitionPosition;
    private Vector3 targetTransitionPosition;
    private float currentTimeTransitioning = 0.0f;
    private float maxTimeTransitioning;

    private void Awake() {
        CurrentTransition = Transition.None;
    }

    private void Update() {
        switch (CurrentTransition) {
            case Transition.In:
                AnimateTransitionIn();
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
        startingTransitionPosition = GetHiddenStreamLocalPosition();
        targetTransitionPosition = GetFullStreamLocalPosition();

        clippingPlane.transform.localPosition = startingTransitionPosition;
        currentTimeTransitioning = 0.0f;
        maxTimeTransitioning = (startingTransitionPosition.y - targetTransitionPosition.y) / PourSpeed;
    }

    public void StopTransitionIn() {
        if (CurrentTransition == Transition.In) {
            CurrentTransition = Transition.None;
        }
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
            TransitionIn();

            // If we're already colliding with the cup when we start showing
            // the stream, lower the cup, since that's what we normally do at
            // the beginning of the collision (which is what it will be from the
            // player's perspective).
            //if (liquidCatcher != null) {
            //    cupEffects.Lower();
            //}
        }
    }

    public void Hide() {
        IsShown = false;
        clippingPlane.transform.localPosition = GetHiddenStreamLocalPosition();
    }

    public void StartBeingCaught(LiquidCatcher catcher) {
        liquidCatcher = catcher;
        cupEffects = catcher.GetComponentInParent<CupEffects>();
    }

    public void StopBeingCaught(LiquidCatcher catcher) {
        liquidCatcher = null;
        cupEffects = null;
    }

    private void AnimateTransitionIn() {
        currentTimeTransitioning += Time.deltaTime;
        float fractionOfJourney = currentTimeTransitioning / maxTimeTransitioning;

        if (fractionOfJourney < 1.0f) {
            clippingPlane.transform.localPosition = Vector3.Lerp(
                GetHiddenStreamLocalPosition(),
                GetFullStreamLocalPosition(),
                currentTimeTransitioning / maxTimeTransitioning
            );
        } else {
            StopTransitionIn();
        }

        // TODO check that we're being caught, end transition early
    }

    private void UpdateClippingPlaneY() {
        if (IsShown) {
            if (liquidCatcher == null) {
                // When the liquid stream isn't being caught, move the clipping
                // plane off-screen so we don't see the stream getting cut off.
                clippingPlane.transform.localPosition = GetFullStreamLocalPosition();
            } else {
                // When the liquid stream is being caught, move the clipping plane
                // to the center of the catcher so the stream is cut off right there.
                //
                // Note: We use position instead of localPosition here because the
                // catcher is parented to the cup rather than the liquid stream.
                clippingPlane.transform.position = GetCupBottomWorldPosition();
            }
        }
    }

    private Vector3 GetHiddenStreamLocalPosition() {
        return new Vector3(
            clippingPlane.transform.localPosition.x,
            1.0f,
            clippingPlane.transform.localPosition.z
        );
    }

    private Vector3 GetFullStreamLocalPosition() {
        return new Vector3(
            clippingPlane.transform.localPosition.x,
            -0.7f,
            clippingPlane.transform.localPosition.z
        );
    }

    private Vector3 GetCupBottomWorldPosition() {
        return new Vector3(
            clippingPlane.transform.position.x,
            liquidCatcher.cupBottom.transform.position.y,
            clippingPlane.transform.position.z
        );
    }
}
