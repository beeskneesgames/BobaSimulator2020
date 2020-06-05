using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSaver : MonoBehaviour {
    private void Start() {
        DontDestroyOnLoad(gameObject);
        Globals.cup = gameObject;
    }
}
