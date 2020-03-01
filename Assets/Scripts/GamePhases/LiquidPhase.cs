using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPhase : GamePhase {
    //public BobaSpawner bobaSpawner;
    public override float Time {
        get {
            return 5.0f;
        }
    }

    protected override void Execute() {
        Debug.Log("In LiquidPhase.Execute()");
        //bobaSpawner.gameObject.SetActive(true);
    }

    public override void EndPhase() {
        Debug.Log("In LiquidPhase.EndPhase()");
        //bobaSpawner.gameObject.SetActive(false);
        //GetComponent<LiquidPhase>().StartPhase();
    }
}
