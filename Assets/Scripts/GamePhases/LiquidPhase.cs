﻿using System.Collections;
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
        StartCoroutine(HideCupAndShowInstructions());
    }

    protected override void ExecuteStart() {
        liquidSpawner.StartSpawning();
        AudioManager.Instance.PlayLiquid();
    }

    protected override void ExecuteEnd() {

    }

    protected override void ExecuteNext() {
        StartCoroutine(ExecuteEndSequence());
    }

    public override bool ShouldEndEarly() {
        return Globals.TotalLiquidPercentage >= 1.0f;
    }

    private IEnumerator HideCupAndShowInstructions() {
        phaseManager.cupAnimator.SetTrigger("HideCup");

        yield return new WaitForSecondsRealtime(1);

        phaseManager.instructionsAnimator.SetTrigger("StartLiquid");
    }

    private IEnumerator ExecuteEndSequence() {
        liquidSpawner.StopSpawning();
        AudioManager.Instance.StopLiquid();
        AudioManager.Instance.StopCupLiquid();

        AudioManager.Instance.PlayYay();

        yield return new WaitForSecondsRealtime(4);

        SceneManager.LoadScene("GradeScene");
    }
}
