using TMPro;
using UnityEngine;

public class GradeScreen : MonoBehaviour {
    public TextMeshProUGUI commentText;
    public TextMeshProUGUI drinkNameText;
    public TextMeshProUGUI letterGradeText;

    private void Start() {
        commentText.text = $"This drink sucks";
        drinkNameText.text = $"Coconut stuff";
        letterGradeText.text = $"F";
    }

    public void RestartGame() {
        GameManager.StartMainScene();
    }

    public void ExitGame() {
        GameManager.ExitGame();
    }
}
