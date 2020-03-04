using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPhase : GamePhase {
    public LiquidSpawner liquidSpawner;

    public override string Name {
        get {
            return "Liquid Phase";
        }
    }
    public override float Time {
        get {
            return 10.0f;
        }
    }

    protected override void ExecuteStart() {
        liquidSpawner.gameObject.SetActive(true);
    }

    protected override void ExecuteEnd() {
        liquidSpawner.gameObject.SetActive(false);
    }

    protected override void StartNextPhase() {
        Debug.Log("NO NEXT PHASE");
    }

    public override bool ShouldEndEarly() {
        return false;
    }
}
