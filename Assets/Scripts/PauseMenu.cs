using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject menuContainer;

    private float oldTimeScale;

    private void Start() {
        oldTimeScale = Time.timeScale;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Globals.isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void OnDestroy() {
        Resume();
    }

    public void Resume() {
        Globals.isPaused = false;
        Time.timeScale = oldTimeScale;

        menuContainer.SetActive(false);
    }

    public void Pause() {
        Globals.isPaused = true;
        oldTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;

        menuContainer.SetActive(true);
    }

    public void Restart() {
        GameManager.StartMainScene();
    }

    public void ExitGame() {
        GameManager.StartTitleScene();
    }
}
