using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabletUI : MonoBehaviour {
    public TextMeshProUGUI orderHeader;
    public TextMeshProUGUI orderText;
    public PhaseManager phaseManager;

    private Order displayedOrder;
    private string displayedPhase;
    private float currentTimeAnimating = 0.0f;
    private float maxTimeAnimating =  0.5f;

    private bool animating = true;

    private void Update() {
        if (displayedOrder != Globals.currentOrder || displayedPhase != phaseManager.CurrentPhase.Name) {
            displayedOrder = Globals.currentOrder;
            displayedPhase = phaseManager.CurrentPhase.Name;
            UpdateDisplayedOrder();
        }
        //if (animating) {
        //    float startingPosition = orderUI.position.x;
        //    float targetPosition = orderUI.position.x;
        //    float fractionOfJourney = currentTimeAnimating / maxTimeAnimating;

        //    currentTimeAnimating += Time.deltaTime;

        //    if (maxTimeAnimating >= 0.0f && fractionOfJourney < 1.0f) {
        //        orderUI.position = new Vector3(
        //            Mathf.Lerp(startingPosition, targetPosition, fractionOfJourney),
        //            orderUI.position.y,
        //            orderUI.position.z
        //        );
        //    } else {
        //        animating = false;
        //    }
        //}
    }

    private void UpdateDisplayedOrder() {
        string[] lines = {
            MarkedLine(displayedOrder.FlavorDescription, displayedPhase == "Liquid Phase"),
            MarkedLine(displayedOrder.BobaDescription, displayedPhase == "Boba Phase"),
            MarkedLine(displayedOrder.IceDescription, displayedPhase == "Ice Phase")
        };
        orderText.text = string.Join("\n", lines);
        orderHeader.text = "Order #" + Globals.orderCount.ToString("D4"); // D4 means "padded with 0s until the string is 4 characters long".
    }

    private static string MarkedLine(string line, bool isMarked) {
        if (isMarked) {
            return $"<mark=#ffff0033>{line}</mark>";
        } else {
            return line;
        }
    }
}
