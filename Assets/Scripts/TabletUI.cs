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
    private Animator orderTextAnimator;

    private void Start() {
        orderTextAnimator = orderText.GetComponent<Animator>();
        UpdateOrderHeader();
    }

    private void Update() {
        if (displayedOrder != Globals.currentOrder || displayedPhase != phaseManager.CurrentPhase.Name) {
            if (displayedOrder != Globals.currentOrder) {
                orderTextAnimator.SetTrigger("ShowOrder");
            }

            displayedOrder = Globals.currentOrder;
            displayedPhase = phaseManager.CurrentPhase.Name;
            UpdateDisplayedOrder();
        }
    }

    private void UpdateDisplayedOrder() {
        string[] lines = {
            MarkedLine(displayedOrder.BobaDescription, displayedPhase == "Boba Phase"),
            MarkedLine(displayedOrder.IceDescription, displayedPhase == "Ice Phase"),
            MarkedLine(displayedOrder.FlavorDescription, displayedPhase == "Liquid Phase")
        };
        orderText.text = string.Join("\n", lines);
    }

    public void UpdateOrderHeader() {
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
