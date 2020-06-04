using TMPro;
using UnityEngine;

public class GradeScreen : MonoBehaviour {
    public TextMeshProUGUI commentText;
    public TextMeshProUGUI drinkNameText;
    public TextMeshProUGUI letterGradeText;

    private void Start() {
        Grade grade = Grade.Compile();
        GameObject cup = Globals.cup;
        float cupScale = 250.0f;

        commentText.text = $"\"{grade.comment}\"";
        drinkNameText.text = grade.drinkName;
        letterGradeText.text = grade.letterGrade.ToString();

        Transform parentTransform = GameObject.Find("GradedCup").transform;

        cup.transform.SetParent(parentTransform);
        cup.transform.SetPositionAndRotation(
            new Vector3(
                parentTransform.position.x,
                parentTransform.position.y,
                parentTransform.position.z + 10.0f
            ),
            new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)
        );

        cup.transform.localScale = new Vector3(cupScale, cupScale, cupScale);

        Destroy(GameObject.Find("ArmCup"));
        Destroy(cup.GetComponent<CupController>());
    }

    public void RestartGame() {
        GameManager.StartMainScene();
    }

    public void ExitGame() {
        GameManager.ExitGame();
    }
}
