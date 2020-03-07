using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
    public Text bobaCountText;
    public Text liquidPercentageText;

    private void Start() {
        bobaCountText.text = $"Boba Count: {Globals.bobaCount}";
        liquidPercentageText.text = $"Liquid Percentage: {Globals.FormattedLiquidPercentage}%";
    }

    public void RestartGame() {
        SceneManager.LoadScene("MainScene");
    }
}
