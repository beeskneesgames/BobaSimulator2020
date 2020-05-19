using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletUI : MonoBehaviour {
    public Text orderText;

    private Order displayedOrder;

    private void Update() {
        if (displayedOrder != Globals.currentOrder) {
            displayedOrder = Globals.currentOrder;
            orderText.text = $"<b>Order</b>\n{OrderBobaText}\n{OrderIceText}\n{OrderFlavorText}";
        }
    }

    private string OrderBobaText {
        get {
            return "";
        }
    }

    private string OrderIceText {
        get {
            return "";
        }
    }

    private string OrderFlavorText {
        get {
            return displayedOrder.drinkFlavors[0].ToString();
        }
    }
}
