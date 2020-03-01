using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebuggerListener {
    void DebuggerToggled();
}

public class Debugger : MonoBehaviour {
    private static Debugger instance;
    public static Debugger Instance {
        get {
            return instance;
        }
    }

    private List<IDebuggerListener> listeners;
    private bool isOn;
    public bool IsOn {
        get {
            return isOn;
        }

        set {
            isOn = value;

            // Notify listeners
            foreach (IDebuggerListener listener in listeners) {
                listener.DebuggerToggled();
            }
        }
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        listeners = new List<IDebuggerListener>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            IsOn = !IsOn;
        }
    }

    public void AddListener(IDebuggerListener listener) {
        listeners.Add(listener);
    }
}
