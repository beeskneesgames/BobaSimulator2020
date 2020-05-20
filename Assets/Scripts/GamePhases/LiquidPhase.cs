using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiquidPhase : GamePhase {
    public LiquidSpawner liquidSpawner;

    public override string Name {
        get {
            return "Liquid Phase";
        }
    }
    public override float Time {
        get {
            // One hour, since we really want this to last until the cup is full
            // of liquid.
            return 3600.0f;
        }
    }

    protected override void ExecuteBeforeStart() {
        phaseManager.instructionsAnimator.SetTrigger("StartLiquid");
    }

    protected override void ExecuteStart() {
        liquidSpawner.StartSpawning();
        AudioManager.Instance.PlayLiquid();
    }

    protected override void ExecuteEnd() {
        liquidSpawner.StopSpawning();
        AudioManager.Instance.StopLiquid();
        AudioManager.Instance.StopCupLiquid();
    }

    protected override void ExecuteNext() {
        SceneManager.LoadScene("FinalScoreScene");
    }

    public override bool ShouldEndEarly() {
        return Globals.TotalLiquidPercentage >= 1.0f;
    }
}
