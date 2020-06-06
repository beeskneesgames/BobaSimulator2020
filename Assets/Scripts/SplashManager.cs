using UnityEngine;

public class SplashManager : MonoBehaviour {
    public SceneFader sceneFader;
    private float timeRemaining = 4.0f;

    private void Update() {
        TickTimer();

        if (timeRemaining <= 0.0f || (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))) {
            sceneFader.FadeOut(() => {
                GameManager.StartIntroCutScene();
            });
        }
    }

    private void TickTimer() {
        timeRemaining -= Time.deltaTime;
    }
}
