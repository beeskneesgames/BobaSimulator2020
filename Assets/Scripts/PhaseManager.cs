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

            int minutesRemaining = (int)TimeRemaining / 60;
            int secondsRemaining = (int)TimeRemaining % 60;
            timeRemainingText.text = $"{minutesRemaining}:{secondsRemaining.ToString("00")}";
        }
    }

    private GamePhase currentPhase;

    private void Start() {
        Debugger.Instance.phaseManager = this;
        GetComponent<BobaPhase>().StartPhase();
    }

    private void Update() {
        if (TimeRemaining > 0.0f) {
            TimeRemaining -= Time.deltaTime;
        } else {
            currentPhase.EndPhase();
        }
    }

    private void OnDestroy() {
        Debugger.Instance.phaseManager = null;
    }

    public void PhaseStarted(GamePhase gamePhase) {
        currentPhase = gamePhase;

        TimeRemaining = currentPhase.Time;
        currentPhaseText.text = currentPhase.Name;
    }

    public void SkipPhase() {
        currentPhase.EndPhase();
    }
}
