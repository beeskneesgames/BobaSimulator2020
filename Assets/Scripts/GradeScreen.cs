﻿using TMPro;
using UnityEngine;

public class GradeScreen : MonoBehaviour {
    public TextMeshProUGUI commentText;
    public TextMeshProUGUI drinkNameText;
    public TextMeshProUGUI letterGradeText;
    public GameObject gradeContainer;

    private void Start() {
        Grade grade = Grade.Compile();
        CupContainer cupContainer = Globals.cupContainer;
        float cupScale = 250.0f;

        commentText.text = $"\"{grade.comment}\"";
        drinkNameText.text = grade.drinkName;
        letterGradeText.text = grade.letterGrade.ToString();

        cupContainer.transform.SetParent(gradeContainer.transform);
        cupContainer.transform.SetPositionAndRotation(
            new Vector3(
                gradeContainer.transform.position.x,
                gradeContainer.transform.position.y,
                gradeContainer.transform.position.z + 10.0f
            ),
            new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)
        );

        cupContainer.transform.localScale = new Vector3(cupScale, cupScale, cupScale);
        cupContainer.PrepareForGradeScreen();
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
