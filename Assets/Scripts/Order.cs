using System;
using System.Collections.Generic;
using System.Linq;
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
    public enum Flavor {
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

    public enum FlavorOption {
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

    public AddInOption iceAmount;
    public AddInOption bobaAmount;
    public FlavorOption drinkType;
    public List<Flavor> drinkFlavors;
    public bool isBubbleTea;

    public Order() {
        iceAmount = GetRandomOption();
        bobaAmount = GetRandomOption();
        drinkType = GetRandomDrinkType();
        drinkFlavors = GetRandomDrinkFlavors();
        isBubbleTea = bobaAmount != AddInOption.None;
    }

    public string ToSentence() {
        return $"{CompileFlavorString()}{CompileIceString()}{CompileBobaString()}";
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

    private FlavorOption GetRandomDrinkType() {
        Array liquidOptions = Enum.GetValues(typeof(FlavorOption));
        FlavorOption randomOption = (FlavorOption)liquidOptions.GetValue(
            UnityEngine.Random.Range(0, liquidOptions.Length - 1)
        );

        return randomOption;
    }

    private List<Flavor> GetRandomDrinkFlavors() {
        int numberOfFlavors = (drinkType == FlavorOption.Single) ? 1 : 2;
        List<Flavor> flavors = new List<Flavor> { };
        Array flavorOptions = Enum.GetValues(typeof(Flavor));

        // TODO: Fix this so it can't repeat the 1st flavor
        for (int i = 0; i < numberOfFlavors; i++) {
            Flavor randomOption = (Flavor)flavorOptions.GetValue(
                UnityEngine.Random.Range(0, flavorOptions.Length - 1)
            );

            flavors.Add(randomOption);
        }

        return flavors;
    }

    private string CompileIceString() {
        if (String.IsNullOrEmpty(iceOrder)) {
            // Mango bubble tea
            return iceOrder;
        } else {
            // Mango bubble tea with light ice
            return $" with {iceOrder}";
        }
    }

    private string CompileBobaString() {
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

    private string CompileFlavorString() {
        Flavor firstFlavor = drinkFlavors.First();
        Flavor secondFlavor = drinkFlavors.Last();
        string teaType = isBubbleTea ? "bubble tea" : "tea";

        string compiledFlavorString;

        switch (drinkType) {
            case FlavorOption.Splash:
                compiledFlavorString = $"{firstFlavor} {teaType} with a splash of ${secondFlavor}";
                break;
            case FlavorOption.Half:
                compiledFlavorString = $"{firstFlavor} ${secondFlavor} {teaType}";
                break;
            default:
                compiledFlavorString = $"{firstFlavor} {teaType}";
                break;
        }

        return $"{compiledFlavorString}";
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
