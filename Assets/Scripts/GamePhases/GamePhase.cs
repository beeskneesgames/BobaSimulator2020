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
    private PhaseManager phaseManager;

    private void Awake() {
        phaseManager = GetComponent<PhaseManager>();
    }

    public void StartPhase() {
        phaseManager.PhaseStarted(this);

        StartCoroutine(DelayPhase(StartDelay, () => {
            ExecuteStart();
        }));
    }

    public void EndPhase() {
        phaseEnding = true;

        ExecuteEnd();

        StartCoroutine(DelayPhase(EndDelay, () => {
            StartNextPhase();
        }));
    }

    private IEnumerator DelayPhase(float delay, System.Action callback) {
        yield return new WaitForSeconds(delay);
        callback();
    }

    protected abstract void ExecuteStart();
    protected abstract void ExecuteEnd();
    protected abstract void StartNextPhase();
}
