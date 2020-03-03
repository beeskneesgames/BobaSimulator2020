using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour {
    public Text currentPhaseText;
    public Text timeRemainingText;
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

    private GamePhase currentPhase;

    private void Start() {
        Debugger.Instance.phaseManager = this;
        GetComponent<BobaPhase>().StartPhase();
    }

    private void Update() {
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

    private void OnDestroy() {
        Debugger.Instance.phaseManager = null;
    }

    public void PhaseStarted(GamePhase gamePhase) {
        currentPhase = gamePhase;

        TimeRemaining = currentPhase.Time + currentPhase.StartDelay + currentPhase.EndDelay;
        currentPhaseText.text = currentPhase.Name;
    }

    public void SkipPhase() {
        TimeRemaining = currentPhase.EndDelay;
    }

    private bool IsInStartDelay() {
        return TimeRemaining >= currentPhase.Time + currentPhase.EndDelay;
    }

    private bool IsInEndDelay() {
        return TimeRemaining < currentPhase.EndDelay;
    }
}
