using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GifRenderer : MonoBehaviour {
    public Sprite[] frames;
    public Image splash;
    private int currentImage;
    private float frameRate;
    private static AsyncOperation loadedTitleScene;

    void Start() {
        loadedTitleScene = SceneManager.LoadSceneAsync("TitleScene", LoadSceneMode.Single);
        loadedTitleScene.allowSceneActivation = false;

        frameRate = 0.05f;
        currentImage = 0;
        InvokeRepeating("ChangeImage", 0.05f, frameRate);
    }

    private void ChangeImage() {
        if (currentImage >= frames.Length - 10) {
            loadedTitleScene.allowSceneActivation = true;
        }

        currentImage += 1;
        splash.sprite = frames[currentImage];
    }
}
