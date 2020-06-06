using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupAnimationListener : MonoBehaviour {
    public PhaseManager phaseManager;

    public void CupEntered() {
        phaseManager.CupShown();
    }

    public void CupExited() {
        phaseManager.CupHidden();
    }
}
