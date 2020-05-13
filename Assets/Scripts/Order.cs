using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour {
    private string iceOrder;
    private string bobaOrder;
    private string flavorOrder;
    private bool isBubbleTea = true;

    static List<string> flavors = new List<string> {
        "Blueberry",
        "Classic Milk Tea",
        "Coconut",
        "Honeydew",
        "Mango",
        "Matcha",
        "Strawberry",
        "Taro",
        "Thai Tea",
    };

    static List<string> liquidOptions = new List<string> {
        "Splash",
        "50/50",
        "Single flavor",
    };

    static List<string> addInOptions = new List<string> {
        "No",
        "Light",
        "Regular",
        "Extra",
    };

    private void Start() {
        iceOrder = CompileAddIns("ice");
        bobaOrder = CompileAddIns("boba");
        flavorOrder = CompileFlavor();
    }

    public string Compile() {
        return $"{iceOrder}{flavorOrder}{bobaOrder}";
    }

    private string CompileIce() {
        if (String.IsNullOrEmpty(iceOrder)) {
            // Mango bubble tea
            return iceOrder;
        } else {
            // Mango bubble tea with light ice
            return $" with {iceOrder}";
        }
    }

    private string CompileBoba() {
        if (String.IsNullOrEmpty(bobaOrder)) {
            // Mango tea
            return bobaOrder;
        } else if (String.IsNullOrEmpty(iceOrder)) {
            // Mango bubble tea with extra boba
            return $" with {bobaOrder}";
        } else {
            // Mango bubble tea with no ice and light boba
            return $" and {bobaOrder}";
        }
    }

    private string CompileFlavor() {
        string chosenFlavor = flavors[UnityEngine.Random.Range(0, flavors.Count - 1)];
        // TODO: Fix this so it can't repeat the chosen flavor
        string secondaryFlavor = flavors[UnityEngine.Random.Range(0, flavors.Count - 1)];
        string liquidOption = liquidOptions[UnityEngine.Random.Range(0, flavors.Count - 1)];
        string teaType = isBubbleTea ? "bubble tea" : "tea";
        string compiledFlavor;

        switch (liquidOption) {
            case "Splash":
                compiledFlavor = $"{chosenFlavor} {teaType} with a splash of ${secondaryFlavor}";
                break;
            case "50/50":
                compiledFlavor = $"{chosenFlavor} ${secondaryFlavor} {teaType}";
                break;
            default:
                compiledFlavor = $"{chosenFlavor} {teaType}";
                break;
        }

        return $"{compiledFlavor}";
    }

    private string CompileAddIns(string addIn) {
        string addInOrder = "";
        string chosenOption = addInOptions[UnityEngine.Random.Range(0, addInOptions.Count - 1)];
        isBubbleTea = true;

        if (chosenOption == "No" && addIn == "boba") {
            isBubbleTea = false;
            return addInOrder;
        };

        if (chosenOption == "Regular") {
            return addInOrder;
        };

        return $"{addInOrder} {addIn}";
    }
}
