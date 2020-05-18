﻿using System.Collections;
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
            return 20.0f;
        }
    }

    protected override void ExecuteBeforeStart() {
        phaseManager.instructionsAnimator.SetTrigger("StartBoba");
        Order order = Order.GenerateRandom();
        Debug.Log(order.ToSentence());
    }

    protected override void ExecuteStart() {
        bobaSpawner.StartSpawning();
    }

    protected override void ExecuteEnd() {
        bobaSpawner.StopSpawning();
    }

    protected override void ExecuteNext() {
        GetComponent<IcePhase>().BeforeStartPhase();
    }

    public override bool ShouldEndEarly() {
        return !bobaPlacer.HasPositions();
    }
}
