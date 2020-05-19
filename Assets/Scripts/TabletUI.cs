using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabletUI : MonoBehaviour {
    public TextMeshProUGUI orderText;

    private Order displayedOrder;

    private void Update() {
        if (displayedOrder != Globals.currentOrder) {
            displayedOrder = Globals.currentOrder;
            orderText.text = $"<b>Order</b>\n{displayedOrder.FlavorDescription}\n{displayedOrder.BobaDescription}\n{displayedOrder.IceDescription}";
        }
    }
}
