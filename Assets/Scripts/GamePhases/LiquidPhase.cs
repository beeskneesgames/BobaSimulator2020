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

    private GameObject cupObj;

    private void Start() {
        cupObj = GameObject.Find("Cup");
    }

    protected override void ExecuteBeforeStart() {
        phaseManager.instructionsAnimator.SetTrigger("StartLiquid");
        DontDestroyOnLoad(cupObj);
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
        Globals.cup = cupObj;

        SceneManager.LoadScene("GradeScene");
    }

    public override bool ShouldEndEarly() {
        return Globals.TotalLiquidPercentage >= 1.0f;
    }
}
