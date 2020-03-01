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

    protected override void Execute() {
        liquidSpawner.gameObject.SetActive(true);
    }

    public override void EndPhase() {
        liquidSpawner.gameObject.SetActive(false);
    }
}
