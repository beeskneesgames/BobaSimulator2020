using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private bool isOn = false;
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

            UpdateGraphy();
        }
    }
    private Tayx.Graphy.GraphyManager graphy;

    public PhaseManager phaseManager;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        listeners = new List<IDebuggerListener>();
    }

    private void Start() {
        graphy = Tayx.Graphy.GraphyManager.Instance;
        UpdateGraphy();
    }

    private void Update() {
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.BackQuote)) {
            IsOn = !IsOn;
        }

        if (IsOn) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                phaseManager.SkipPhase();
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            StartCoroutine(TakeScreenshot());
        }
    }

    public void AddListener(IDebuggerListener listener) {
        listeners.Add(listener);
    }

    private void UpdateGraphy() {
        if (graphy != null) {
            graphy.gameObject.SetActive(IsOn);
        }
    }

    private IEnumerator TakeScreenshot() {
        yield return new WaitForEndOfFrame();

        // Get image from screen.
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        // Write to file
        string desktopDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        DirectoryInfo fileDir = Directory.CreateDirectory($"{desktopDir}/boba-screenshots");
        string filename = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string filepath = $"{fileDir.FullName}/{filename}.png";
        System.IO.File.WriteAllBytes(filepath, screenImage.EncodeToPNG());
    }
}
