using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour, IDebuggerListener {
    public Text bobaCount;
    public Text currentScene;
    public Text timeRemainingText;

    private void Start() {
        Debugger.Instance.AddListener(this);
        UpdateVisibilities();
    }

    public void DebuggerToggled() {
        UpdateVisibilities();
    }

    private void UpdateVisibilities() {
        bobaCount.gameObject.SetActive(Debugger.Instance.IsOn);
        currentScene.gameObject.SetActive(Debugger.Instance.IsOn);
        timeRemainingText.gameObject.SetActive(Debugger.Instance.IsOn);
    }
}
