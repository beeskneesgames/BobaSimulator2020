﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
    public Text bobaCountText;
    public Text liquidPercentageText;

    void Start() {
        bobaCountText.text = $"Boba Count: {Globals.bobaCount}";
        liquidPercentageText.text = $"Liquid Percentage: {Globals.FormattedLiquidPercentage}%";
    }
}
