using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabletUI : MonoBehaviour {
    public TextMeshProUGUI orderText;
    public PhaseManager phaseManager;

    private Order displayedOrder;
    private string displayedPhase;
    private float currentTimeAnimating = 0.0f;
    private float maxTimeAnimating =  0.5f;

    Transform panel;
    Transform orderUI;

    private bool animating = true;

    private void Start() {
        panel = transform.Find("Panel");
        orderUI = panel.Find("OrderUI");
    }

    private void Update() {
        if (animating) {
            float startingPosition = orderUI.position.x;
            float targetPosition = orderUI.position.x;
            float fractionOfJourney = currentTimeAnimating / maxTimeAnimating;

            currentTimeAnimating += Time.deltaTime;

            if (maxTimeAnimating >= 0.0f && fractionOfJourney < 1.0f) {
                orderUI.position = new Vector3(
                    Mathf.Lerp(startingPosition, targetPosition, fractionOfJourney),
                    orderUI.position.y,
                    orderUI.position.z
                );
            } else {
                animating = false;
            }
        }

        if (displayedOrder != Globals.currentOrder || displayedPhase != phaseManager.CurrentPhase.Name) {
            displayedOrder = Globals.currentOrder;
            displayedPhase = phaseManager.CurrentPhase.Name;

            string[] lines = {
                MarkedLine(displayedOrder.FlavorDescription.ToUpper(), displayedPhase == "Liquid Phase"),
                MarkedLine(displayedOrder.BobaDescription.ToUpper(), displayedPhase == "Boba Phase"),
                MarkedLine(displayedOrder.IceDescription.ToUpper(), displayedPhase == "Ice Phase")
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
