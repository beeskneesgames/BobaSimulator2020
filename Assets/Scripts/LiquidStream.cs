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
    public Order.Flavor CurrentFlavor {
        get;
        private set;
    }
    public Transition CurrentTransition {
        get;
        private set;
    }

    public bool IsShown {
        get;
        private set;
    }

    private Vector3 clippingPlaneStartingLocalPosition;
    private Vector3 clippingPlanePreTransitionPosition;
    private Vector3 clippingPlanePostTransitionPosition;
    private float currentTimeTransitioning = 0.0f;
    private float maxTimeTransitioning;

    private Vector3 preTransitionPosition;
    private Vector3 postTransitionPosition;
    private Vector3 nextPosition;

    private void Awake() {
        CurrentTransition = Transition.None;
    }

    private void Start() {
        clippingPlaneStartingLocalPosition = clippingPlane.transform.localPosition;
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

    public void MoveTo(Vector3 newPosition) {
        nextPosition = newPosition;
        TransitionOut();
    }

    public void TransitionIn() {
        CurrentFlavor = Globals.currentOrder.drinkFlavors[0];
        GetComponent<Renderer>().material.SetColor("_BaseColor", Order.FlavorColors[CurrentFlavor]);
        CurrentTransition = Transition.In;
        clippingPlanePreTransitionPosition = GetClippingPlaneHiddenPosition();
        clippingPlanePostTransitionPosition = GetClippingPlaneFullPosition();

        clippingPlane.transform.position = clippingPlanePreTransitionPosition;
        currentTimeTransitioning = 0.0f;
        maxTimeTransitioning = (clippingPlanePreTransitionPosition.y - clippingPlanePostTransitionPosition.y) / PourSpeed;
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
        preTransitionPosition = transform.position;
        postTransitionPosition = GetLiquidStreamHiddenPosition();
        currentTimeTransitioning = 0.0f;
        maxTimeTransitioning = (preTransitionPosition.y - postTransitionPosition.y) / PourSpeed;
    }

    public void StopTransitionOut() {
        transform.position = nextPosition;
        clippingPlane.transform.localPosition = clippingPlaneStartingLocalPosition;
        TransitionIn();
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

        if (CurrentTransition == Transition.Out) {
            StopTransitionOut();
        }
    }

    private void AnimateTransitionIn() {
        currentTimeTransitioning += Time.deltaTime;
        float fractionOfJourney = currentTimeTransitioning / maxTimeTransitioning;

        if (fractionOfJourney < 1.0f) {
            clippingPlane.transform.position = Vector3.Lerp(
                clippingPlanePreTransitionPosition,
                clippingPlanePostTransitionPosition,
                currentTimeTransitioning / maxTimeTransitioning
            );
        } else {
            StopTransitionIn();
        }
    }

    private void AnimateTransitionOut() {
        UpdateClippingPlaneY();

        currentTimeTransitioning += Time.deltaTime;
        float fractionOfJourney = currentTimeTransitioning / maxTimeTransitioning;

        if (fractionOfJourney < 1.0f) {
            Vector3 oldClippingPlanePosition = clippingPlane.transform.position;

            transform.position = Vector3.Lerp(
                preTransitionPosition,
                postTransitionPosition,
                currentTimeTransitioning / maxTimeTransitioning
            );

            // After moving the stream down, restore the clipping plane's old
            // world position, since we might want to keep it looking like it's
            // being caught by the cup.
            clippingPlane.transform.position = oldClippingPlanePosition;
        } else {
            StopTransitionOut();
        }
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

    private Vector3 GetLiquidStreamHiddenPosition() {
        return new Vector3(
            transform.position.x,
            -10.5f,
            transform.position.z
        );
    }
}
