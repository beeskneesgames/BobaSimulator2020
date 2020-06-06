using System.Collections;
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
            if (Globals.currentOrder.iceAmount == Order.AddInOption.None ||
                Globals.currentOrder.iceAmount == Order.AddInOption.Light) {
                return 10.0f;
            } else if (Globals.currentOrder.iceAmount == Order.AddInOption.Regular) {
                return 15.0f;
            } else {
                return 20.0f;
            }
        }
    }

    protected override void ExecuteBeforeStart() {
        StartCoroutine(HideCupAndShowInstructions());
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

    private IEnumerator HideCupAndShowInstructions() {
        if (phaseManager.IsCupShown) {
            phaseManager.HideCup();
        }

        yield return new WaitForSecondsRealtime(1);

        phaseManager.instructionsAnimator.SetTrigger("StartIce");
    }
}
