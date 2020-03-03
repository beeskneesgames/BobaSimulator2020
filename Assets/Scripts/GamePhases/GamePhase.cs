using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GamePhase : MonoBehaviour {
    public virtual string Name { get; }
    public virtual float Time { get; }
    private PhaseManager phaseManager;

    private void Awake() {
        phaseManager = GetComponent<PhaseManager>();
    }

    public void StartPhase() {
        phaseManager.PhaseStarted(this);
        Execute();
    }

    protected abstract void Execute();
    public abstract void EndPhase();
}
