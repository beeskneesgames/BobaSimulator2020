using TMPro;
using UnityEngine;

public class GradeScreen : MonoBehaviour {
    public TextMeshProUGUI commentText;
    public TextMeshProUGUI drinkNameText;
    public TextMeshProUGUI letterGradeText;

    private void Start() {
        Grade grade = Grade.Compile();

        commentText.text = $"\"{grade.comment}\"";
        drinkNameText.text = grade.drinkName;
        letterGradeText.text = grade.letterGrade.ToString();
    }

    public void RestartGame() {
        GameManager.StartMainScene();
    }

    public void ExitGame() {
        GameManager.ExitGame();
    }
}
