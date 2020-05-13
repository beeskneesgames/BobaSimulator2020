using System;
using System.Collections.Generic;
using UnityEngine;

// This class creates strings for a person's order

// For example:
// Mango tea 
// Mango bubble tea
// Mango bubble tea with light ice
// Mango bubble tea with light boba
// Mango bubble tea with no ice and extra boba

// Mango Blueberry bubble tea
// Mango with a splash of Blueberry tea

public class Order : MonoBehaviour {
    public enum Flavors {
        Blueberry,
        Classic,
        Coconut,
        Honeydew,
        Mango,
        Matcha,
        Strawberry,
        Taro,
        Thai,
    };

    public enum LiquidOption {
        Splash,
        Half,
        Single,
    };

    public enum AddInOption {
        None,
        Light,
        Regular,
        Extra,
    };

    public AddInOption IceAmount;
    public AddInOption BobaAmount;
    public LiquidOption DrinkType;

    public Order() {
        IceAmount = GetRandomOption();
        BobaAmount = GetRandomOption();
        DrinkType = GetRandomDrinkType();
    }

    public string ToString() {
        return $"{CompileFlavor()}{CompileIce()}{CompileBoba()}";
    }

    private bool isBubbleTea() {
        return BobaAmount != AddInOption.None;
    }

    // TODO: These two random methods are the same but the types are diff
    //       Is there a way to combine without it being messy?

    private AddInOption GetRandomOption() {
        Array addInOptions = Enum.GetValues(typeof(AddInOption));
        AddInOption randomOption = (AddInOption)addInOptions.GetValue(
            UnityEngine.Random.Range(0, addInOptions.Length - 1)
        );

        return randomOption;
    }

    private LiquidOption GetRandomDrinkType() {
        Array liquidOptions = Enum.GetValues(typeof(LiquidOption));
        LiquidOption randomOption = (LiquidOption)liquidOptions.GetValue(
            UnityEngine.Random.Range(0, liquidOptions.Length - 1)
        );

        return randomOption;
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
