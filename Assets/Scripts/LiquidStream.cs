using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidStream : MonoBehaviour {
    private const float PourSpeed = 50.0f;

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
    private Vector3 nextPosition;

    private void Awake() {
        CurrentTransition = Transition.None;
    }

    public void MoveTo(Vector3 newPosition) {
        nextPosition = newPosition;
        //TransitionOut();
    }

    private void Update() {
        switch (CurrentTransition) {
            case Transition.In:
                AnimateTransitionIn();
                break;
            case Transition.Out:
                AnimateTransitionOut();
                break;
            case Transition.None:
                UpdateClippingPlaneY();
                break;
        }
    }

    public void TransitionIn() {
        CurrentTransition = Transition.In;
        startingTransitionPosition = GetClippingPlaneHiddenPosition();
        targetTransitionPosition = GetClippingPlaneFullPosition();

        clippingPlane.transform.position = startingTransitionPosition;
        currentTimeTransitioning = 0.0f;
        maxTimeTransitioning = (startingTransitionPosition.y - targetTransitionPosition.y) / PourSpeed;
    }

    public void StopTransitionIn() {
        if (CurrentTransition == Transition.In) {
            CurrentTransition = Transition.None;

            if (liquidCatcher != null) {
                cupEffects.Lower();
            }
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
        }
    }

    public void Hide() {
        IsShown = false;
        clippingPlane.transform.position = GetClippingPlaneHiddenPosition();
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
            clippingPlane.transform.position = Vector3.Lerp(
                GetClippingPlaneHiddenPosition(),
                GetClippingPlaneFullPosition(),
                currentTimeTransitioning / maxTimeTransitioning
            );
        } else {
            StopTransitionIn();
        }
    }

    private void AnimateTransitionOut() {
        //currentTimeTransitioning += Time.deltaTime;
        //float fractionOfJourney = currentTimeTransitioning / maxTimeTransitioning;

        //if (fractionOfJourney < 1.0f) {
        //    clippingPlane.transform.position = Vector3.Lerp(
        //        GetClippingPlaneHiddenPosition(),
        //        GetClippingPlaneFullPosition(),
        //        currentTimeTransitioning / maxTimeTransitioning
        //    );
        //} else {
        //    StopTransitionIn();
        //}
    }

    private void UpdateClippingPlaneY() {
        if (IsShown) {
            if (liquidCatcher == null) {
                // When the liquid stream isn't being caught, move the clipping
                // plane off-screen so we don't see the stream getting cut off.
                clippingPlane.transform.position = GetClippingPlaneFullPosition();
            } else {
                // When the liquid stream is being caught, move the clipping plane
                // to the center of the catcher so the stream is cut off right there.
                //
                // Note: We use position instead of localPosition here because the
                // catcher is parented to the cup rather than the liquid stream.
                clippingPlane.transform.position = GetClippingPlaneCaughtPosition();
            }
        }
    }

    private Vector3 GetClippingPlaneHiddenPosition() {
        return new Vector3(
            clippingPlane.transform.position.x,
            6.0f,
            clippingPlane.transform.position.z
        );
    }

    private Vector3 GetClippingPlaneFullPosition() {
        return new Vector3(
            clippingPlane.transform.position.x,
            -4.0f,
            clippingPlane.transform.position.z
        );
    }

    private Vector3 GetClippingPlaneCaughtPosition() {
        return new Vector3(
            clippingPlane.transform.position.x,
            liquidCatcher.cupBottom.transform.position.y,
            clippingPlane.transform.position.z
        );
    }
}
