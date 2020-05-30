using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePhase : GamePhase {
    public IcePlacer icePlacer;
    public IceSpawner iceSpawner;

    public override float EndDelay {
        get {
            return 2.0f;
        }
    }
    public override string Name {
        get {
            return "Ice Phase";
        }
    }
    public override float Time {
        get {
            return 10.0f;
        }
    }

    protected override void ExecuteBeforeStart() {
        phaseManager.instructionsAnimator.SetTrigger("StartIce");
    }

    protected override void ExecuteStart() {
        iceSpawner.StartSpawning();
    }

    protected override void ExecuteEnd() {
        iceSpawner.StopSpawning();
    }

    protected override void ExecuteNext() {
        GetComponent<LiquidPhase>().BeforeStartPhase();
    }

    public override bool ShouldEndEarly() {
        return !icePlacer.HasPositions();
    }
}
