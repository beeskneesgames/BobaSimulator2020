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
            return 10.0f;
        }
    }

    protected override void ExecuteStart() {
        liquidSpawner.gameObject.SetActive(true);
    }

    protected override void ExecuteEnd() {
        liquidSpawner.gameObject.SetActive(false);
    }

    protected override void ExecuteNext() {
        SceneManager.LoadScene("FinalScoreScene");
    }

    public override bool ShouldEndEarly() {
        return false;
    }
}
