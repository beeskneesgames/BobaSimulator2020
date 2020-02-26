using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    public Text bobaCount;

    private void Start() {
        if (GlobalVariables.isDebug) {
            bobaCount.gameObject.SetActive(true);
        } else {
            bobaCount.gameObject.SetActive(false);
        }
    }
}
