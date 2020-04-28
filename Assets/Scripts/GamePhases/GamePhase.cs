using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GamePhase : MonoBehaviour {
    public virtual string Name { get; }
    public virtual float Time { get; }
    public virtual float StartDelay {
        get {
            return 0.0f;
        }
    }
    public virtual float EndDelay {
        get {
            return 0.0f;
        }
    }
    public bool phaseEnding = false;
    protected PhaseManager phaseManager;

    private void Awake() {
        phaseManager = GetComponent<PhaseManager>();
    }

    public void BeforeStartPhase() {
        phaseManager.CurrentPhase = this;
        ExecuteBeforeStart();
    }

    public void StartPhase() {
        phaseManager.CurrentPhaseStarted();

        StartCoroutine(DelayPhase(StartDelay, () => {
            ExecuteStart();
        }));
    }

    public void EndPhase() {
        phaseEnding = true;

        ExecuteEnd();

        StartCoroutine(DelayPhase(EndDelay, () => {
            ExecuteNext();
        }));
    }

    public void EndPhaseEarly() {
        phaseManager.SkipPhase();
    }

    private IEnumerator DelayPhase(float delay, System.Action callback) {
        yield return new WaitForSeconds(delay);
        callback();
    }

    protected virtual void ExecuteBeforeStart() {
        // Override this if you want to play an animation or something before
        // the timer starts. Do NOT call super(), since it'll start the phase
        // right away.
        StartPhase();
    }

    protected abstract void ExecuteStart();
    protected abstract void ExecuteEnd();
    protected abstract void ExecuteNext();

    public abstract bool ShouldEndEarly();
}
