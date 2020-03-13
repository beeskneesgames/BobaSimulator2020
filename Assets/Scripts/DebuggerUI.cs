using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggerUI : MonoBehaviour, IDebuggerListener {
    public Text bobaCount;
    public Text iceCount;
    public Text liquidPercentage;

    public Text currentPhase;
    public Text timeRemainingText;

    public Text hotkeys;

    private void Start() {
        Debugger.Instance.AddListener(this);
        UpdateVisibilities();
    }

    public void DebuggerToggled() {
        UpdateVisibilities();
    }

    private void UpdateVisibilities() {
        bobaCount.gameObject.SetActive(Debugger.Instance.IsOn);
        iceCount.gameObject.SetActive(Debugger.Instance.IsOn);
        currentPhase.gameObject.SetActive(Debugger.Instance.IsOn);
        hotkeys.gameObject.SetActive(Debugger.Instance.IsOn);
        liquidPercentage.gameObject.SetActive(Debugger.Instance.IsOn);
        timeRemainingText.gameObject.SetActive(Debugger.Instance.IsOn);
    }
}
