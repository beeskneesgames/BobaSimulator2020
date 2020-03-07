using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPhase : GamePhase {
    public BobaPlacer bobaPlacer;
    public BobaSpawner bobaSpawner;

    public override float EndDelay {
        get {
            return 2.0f;
        }
    }
    public override string Name {
        get {
            return "Boba Phase";
        }
    }
    public override float Time {
        get {
            return 10.0f;
        }
    }

    protected override void ExecuteStart() {
        bobaSpawner.gameObject.SetActive(true);
    }

    protected override void ExecuteEnd() {
        bobaSpawner.gameObject.SetActive(false);
    }

    protected override void ExecuteNext() {
        GetComponent<LiquidPhase>().StartPhase();
    }

    public override bool ShouldEndEarly() {
        return !bobaPlacer.HasPositions();
    }
}
