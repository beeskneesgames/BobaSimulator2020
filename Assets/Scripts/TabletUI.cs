using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabletUI : MonoBehaviour {
    public TextMeshProUGUI orderText;
    public PhaseManager phaseManager;

    private Order displayedOrder;
    private string displayedPhase;

    private void Update() {
        if (displayedOrder != Globals.currentOrder || displayedPhase != phaseManager.CurrentPhase.Name) {
            displayedOrder = Globals.currentOrder;
            displayedPhase = phaseManager.CurrentPhase.Name;

            string[] lines = {
                "<b>Order</b>",
                MarkedLine(displayedOrder.FlavorDescription, displayedPhase == "Liquid Phase"),
                MarkedLine(displayedOrder.BobaDescription, displayedPhase == "Boba Phase"),
                MarkedLine(displayedOrder.IceDescription, displayedPhase == "Ice Phase")
            };
            orderText.text = string.Join("\n", lines);
        }
    }

    private static string MarkedLine(string line, bool isMarked) {
        if (isMarked) {
            return $"<mark=#ffff0033>{line}</mark>";
        } else {
            return line;
        }
    }
}
