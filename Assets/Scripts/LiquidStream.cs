using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LiquidStream : MonoBehaviour {
    private const float PourSpeed = 75.0f;
    private const float CorrectFlavorChance = 0.5f;
    private static readonly int VFXColorProp = Shader.PropertyToID("Color");

    public enum Transition {
        In,
        Out,
        None
    }

    public LiquidCatcher liquidCatcher;
    public CupEffects cupEffects;
    public ClippingPlane clippingPlane;
    public VisualEffect splashEffect;
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

    private System.Array possibleFlavors;

    private void Awake() {
        CurrentTransition = Transition.None;
        possibleFlavors = System.Enum.GetValues(typeof(Order.Flavor));
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
                UpdateSplashEffect();
                break;
        }
    }

    public void MoveTo(Vector3 newPosition) {
        nextPosition = newPosition;
        TransitionOut();
    }

    public void TransitionIn() {
        int flavorIndex;
        float diceRoll = Random.value;

        if (diceRoll < CorrectFlavorChance) {
            // Sometimes, randomly pick one of the flavors in the order.
            flavorIndex = Random.Range(0, Globals.currentOrder.drinkFlavors.Count);
            CurrentFlavor = Globals.currentOrder.drinkFlavors[flavorIndex];
        } else {
            // Other times, pick a random flavor that may not be in the order.
            // Skip 0 since that's NotSet.
            flavorIndex = Random.Range(1, possibleFlavors.Length);
            CurrentFlavor = (Order.Flavor)possibleFlavors.GetValue(flavorIndex);
        }

        Color flavorColor = Order.FlavorColors[CurrentFlavor];
        GetComponent<Renderer>().material.SetColor("_BaseColor", flavorColor);
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
        HideSplash();
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
        if (splashEffect.gameObject.activeInHierarchy) {
            HideSplash();
        }

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

        if (splashEffect.gameObject.activeInHierarchy) {
            HideSplash();
        }

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

    private void UpdateSplashEffect() {
        if (IsShown && liquidCatcher) {
            if (!splashEffect.gameObject.activeInHierarchy) {
                ShowSplash();
            }

            splashEffect.SetVector4(VFXColorProp, Globals.LiquidFillColor);
            splashEffect.transform.position = new Vector3(
                splashEffect.transform.position.x,
                liquidCatcher.liquidFillClippingPlane.transform.position.y - 0.025f,
                splashEffect.transform.position.z
            );
        } else if (splashEffect.gameObject.activeInHierarchy) {
            HideSplash();
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

    private void ShowSplash() {
        splashEffect.gameObject.SetActive(true);
    }

    private void HideSplash() {
        splashEffect.gameObject.SetActive(false);
    }
}
