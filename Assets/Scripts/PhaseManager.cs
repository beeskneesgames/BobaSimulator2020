﻿using System.Collections;
using UnityEngine;

public class PhaseManager : MonoBehaviour {
    public Animator cupAnimator;
    public Animator instructionsAnimator;

    public GamePhase startingPhase;
    public float timeRemaining;
    public float TimeRemaining {
        get {
            return timeRemaining;
        }

        set {
            if (value > 0.0f) {
                timeRemaining = value;
            } else {
                timeRemaining = 0.0f;
            }
        }
    }

    private bool timerActive = false;
    private GamePhase currentPhase;
    public GamePhase CurrentPhase {
        get {
            return currentPhase;
        }

        set {
            currentPhase = value;
        }
    }
    public bool IsCupShown {
        get;
        private set;
    }

    private void Start() {
        Debugger.Instance.phaseManager = this;

        // Reset most globals
        Globals.ResetLiquid();
        Globals.bobaCount = 0;
        Globals.iceCount = 0;

        Globals.orderCount++;

        if (Globals.orderCount > 1) {
            Globals.currentOrder = Order.GenerateRandom();
        } else {
            Globals.currentOrder = Order.GenerateBasic();
        }

        IsCupShown = false;

        startingPhase.BeforeStartPhase();
    }

    private void Update() {
        if (timerActive) {
            // If the phase timer is active, tick it down and then pause it if
            // the time is up.
            TickTimer();

            if (TimeRemaining <= 0.0f) {
                timerActive = false;
            }
        } else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            if (Debugger.Instance.IsOn) {
                // If the phase timer is inactive, it means we're playing an
                // instructions animation.
                //
                // While in one of these animations, if the player clicks, fast
                // forward the animation.
                instructionsAnimator.SetTrigger("Hide");
            }
        }
    }

    private void OnDestroy() {
        Debugger.Instance.phaseManager = null;
    }

    public void InstructionsHidden() {
        StartCoroutine(ShowCup());
    }

    public void CurrentPhaseStarted() {
        TimeRemaining = currentPhase.Time + currentPhase.StartDelay + currentPhase.EndDelay;
        timerActive = true;
    }

    public void SkipPhase() {
        if (timerActive && !currentPhase.phaseEnding) {
            TimeRemaining = currentPhase.EndDelay;
        }
    }

    public void CupHidden() {
        IsCupShown = false;
    }

    public void CupShown() {
        IsCupShown = true;
        currentPhase.StartPhase();
    }

    public void HideCup() {
        cupAnimator.SetTrigger("HideCup");
    }

    private IEnumerator ShowCup() {
        yield return new WaitForSecondsRealtime(1);
        cupAnimator.SetTrigger("ShowCup");
    }

    private bool IsInStartDelay() {
        return TimeRemaining >= currentPhase.Time + currentPhase.EndDelay;
    }

    private bool IsInEndDelay() {
        return TimeRemaining <= currentPhase.EndDelay;
    }

    private void TickTimer() {
        TimeRemaining -= Time.deltaTime;

        if (IsInEndDelay()) {
            if (!currentPhase.phaseEnding) {
                currentPhase.EndPhase();
            }
        }
    }
}
