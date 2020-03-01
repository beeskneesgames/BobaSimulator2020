using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GamePhase : MonoBehaviour {
    public virtual float Time { get; }
    private PhaseManager phaseManager;

    private void Start() {
        phaseManager = GetComponent<PhaseManager>();
    }

    public void StartPhase() {
        phaseManager.PhaseStarted(this);
        Execute();
    }

    protected abstract void Execute();
}
