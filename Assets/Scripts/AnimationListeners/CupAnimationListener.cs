using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupAnimationListener : MonoBehaviour {
    public PhaseManager phaseManager;

    public void CupEntered() {
        phaseManager.CupShown();
    }

    public void CupExited() {
        // We don't do anything here right now, but we may need to in the near
        // future.
    }
}
