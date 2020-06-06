using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour {
    private Animator animator;
    private System.Action fadeOutCallback;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void FadeOut(System.Action callback) {
        animator.SetTrigger("FadeOut");
        fadeOutCallback = callback; 
    }

    public void OnFadeComplete() {
        fadeOutCallback?.Invoke();
    }
}
