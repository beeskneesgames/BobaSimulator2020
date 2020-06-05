using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupContainer : MonoBehaviour {
    public GameObject arm;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        Globals.cupContainer = this;
    }

    public void PrepareForGradeScreen() {
        Destroy(arm);
        Destroy(GetComponentInChildren<CupController>());
    }
}
