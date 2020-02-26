using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    public Text bobaCount;
    public Text timeRemainingText;

    private void Start() {
        bobaCount.gameObject.SetActive(GlobalVariables.isDebug);
        timeRemainingText.gameObject.SetActive(GlobalVariables.isDebug);
    }
}
