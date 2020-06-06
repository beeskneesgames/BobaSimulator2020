using TMPro;
using UnityEngine;

public class GradeScreen : MonoBehaviour {
    public TextMeshProUGUI commentText;
    public TextMeshProUGUI drinkNameText;
    public TextMeshProUGUI letterGradeText;
    public GameObject gradeContainer;
    public GameObject sparkles;

    private void Start() {
        Grade grade = Grade.Compile();
        CupContainer cupContainer = Globals.cupContainer;
        float cupScale = 250.0f;

        commentText.text = $"\"{grade.comment}\"";
        drinkNameText.text = grade.drinkName;
        letterGradeText.text = grade.letterGrade.ToString();

        cupContainer.transform.SetParent(gradeContainer.transform);
        cupContainer.transform.localPosition = new Vector3(40.0f, 0.0f, 0.0f);
        cupContainer.transform.rotation = Quaternion.identity;
        cupContainer.transform.Rotate(Vector3.forward * -7.5f);

        cupContainer.transform.localScale = new Vector3(cupScale, cupScale, cupScale);
        cupContainer.PrepareForGradeScreen();

        if (grade.letterGrade == Grade.LetterGrade.A) {
            sparkles.SetActive(true);
        };
    }

    public void RestartGame() {
        DestroyCup();
        GameManager.StartMainScene();
    }

    public void ExitGame() {
        DestroyCup();
        GameManager.ExitGame();
    }

    private void DestroyCup() {
        Destroy(Globals.cupContainer);
        Globals.cupContainer = null;
    }
}
