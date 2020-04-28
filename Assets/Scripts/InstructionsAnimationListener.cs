using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsAnimationListener : MonoBehaviour {
    public PhaseManager phaseManager;

    public void BobaAnimationEnded() {
        phaseManager.InstructionsHidden();
    }

    public void IceAnimationEnded() {
        phaseManager.InstructionsHidden();
    }

    public void LiquidAnimationEnded() {
        phaseManager.InstructionsHidden();
    }
}
