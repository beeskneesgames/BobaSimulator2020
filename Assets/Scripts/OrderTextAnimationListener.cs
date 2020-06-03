using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTextAnimationListener : MonoBehaviour {
    public TabletUI tabletUI;

    public void EnterAnimationEnded() {
        tabletUI.UpdateOrderHeader();
    }
}
