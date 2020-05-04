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

    public ClippingPlane clippingPlane;
    public Transition CurrentTransition {
        get;
        private set;
    }

    public void Awake() {
        CurrentTransition = Transition.None;
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
}
