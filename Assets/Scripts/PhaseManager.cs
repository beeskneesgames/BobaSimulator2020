using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour {
    public Animator cupAnimator;
    public Animator instructionsAnimator;

    public Text currentPhaseText;
    public Text timeRemainingText;
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

            // Add 1 to time remaining to make it easier to read
            float timeRemainingToDisplay = TimeRemaining + 1.0f;
            int minutesRemaining = (int)timeRemainingToDisplay / 60;
            int secondsRemaining = (int)timeRemainingToDisplay % 60;
            timeRemainingText.text = $"{minutesRemaining}:{secondsRemaining.ToString("00")}";
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
        currentPhaseText.text = currentPhase.Name;
        timerActive = true;
    }

    public void SkipPhase() {
        if (timerActive && !currentPhase.phaseEnding) {
            TimeRemaining = currentPhase.EndDelay;
        }
    }

    public void CupShown() {
        currentPhase.StartPhase();
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

        if (IsInStartDelay() || IsInEndDelay()) {
            timeRemainingText.color = Color.red;
        } else {
            timeRemainingText.color = Color.white;
        }
    }
}
