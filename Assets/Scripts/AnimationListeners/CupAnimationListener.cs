using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupAnimationListener : MonoBehaviour {
    public Animator animator;
    public PhaseManager phaseManager;
    public bool IsShown {
        get;
        private set;
    }

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void CupEntered() {
        animator.StopPlayback();
        animator.enabled = false;
        IsShown = true;
        phaseManager.CupShown();
    }

    public void CupExited() {
        IsShown = false;
        phaseManager.CupHidden();
    }
}
