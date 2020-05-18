using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Order {
    public enum Flavor {
        Blueberry,
        Coconut,
        Coffee,
        Honeydew,
        Lemon,
        Mango,
        Matcha,
        Strawberry,
        Taro,
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

    public readonly static Dictionary<Flavor, Color> FlavorColors = new Dictionary<Flavor, Color>() {
        { Flavor.Blueberry,  new Color(0.31f,   0.53f,   0.97f  ) },
        { Flavor.Coconut,    new Color(0.96f,   0.96f,   0.96f  ) },
        { Flavor.Coffee,     new Color(0.44f,   0.31f,   0.22f  ) },
        { Flavor.Honeydew,   new Color(0.749f,  1.0f,    0.729f ) },
        { Flavor.Lemon,      new Color(1.0f,    1.0f,    0.4f   ) },
        { Flavor.Mango,      new Color(1.0f,    0.51f,   0.26f  ) },
        { Flavor.Matcha,     new Color(0.4118f, 0.749f,  0.3922f) },
        { Flavor.Strawberry, new Color(0.7843f, 0.2471f, 0.2863f) },
        { Flavor.Taro,       new Color(0.7412f, 0.4863f, 0.7765f) },
    };

    public AddInOption iceAmount;
    public AddInOption bobaAmount;
    public FlavorOption drinkType;
    public List<Flavor> drinkFlavors;

    public bool IsBubbleTea() {
        return bobaAmount != AddInOption.None;
    }

    public string ToSentence() {
        // This method returns a person's order as a readable sentence

        // For example:
        // Mango tea 
        // Mango bubble tea
        // Mango bubble tea with light ice
        // Mango bubble tea with light boba
        // Mango bubble tea with no ice and extra boba

        // Mango blueberry bubble tea
        // Mango with a splash of blueberry tea

        return $"{CompileFlavorString()}{CompileIceString()}{CompileBobaString()}";
    }

    private string CompileIceString() {
        string compiledString;
        string iceAmountStr = iceAmount.ToString().ToLower();

        switch (iceAmount) {
            case AddInOption.None:
                // Mango bubble tea with no ice
                compiledString = " with no ice";
                break;
            case AddInOption.Regular:
                // Mango bubble tea
                compiledString = "";
                break;
            default:
                // Mango bubble tea with light ice
                compiledString = $" with {iceAmountStr} ice";
                break;
        }

        return compiledString;
    }

    private string CompileBobaString() {
        string compiledString;
        string bobaAmountStr = bobaAmount.ToString().ToLower();

        if (bobaAmount == AddInOption.None || bobaAmount == AddInOption.Regular) {
            // Mango tea
            compiledString = "";
        } else if (iceAmount == AddInOption.Regular) {
            // Mango bubble tea with extra boba
            compiledString = $" with {bobaAmountStr} boba";
        } else {
            // Mango bubble tea with no ice and light boba
            compiledString = $" and {bobaAmountStr} boba";
        }

        return compiledString;
    }

    private string CompileFlavorString() {
        Flavor firstFlavor = drinkFlavors.First();
        Flavor secondFlavor = drinkFlavors.Last();
        string secondFlavorStr = secondFlavor.ToString().ToLower();
        string teaType = IsBubbleTea() ? "bubble tea" : "tea";

        string compiledString;

        switch (drinkType) {
            case FlavorOption.Splash:
                // Mango with a splash of lemon tea
                compiledString = $"{firstFlavor} with a splash of {secondFlavorStr} {teaType}";
                break;
            case FlavorOption.Half:
                // Mango lemon tea
                compiledString = $"{firstFlavor} {secondFlavorStr} {teaType}";
                break;
            default:
                // Mango tea
                compiledString = $"{firstFlavor} {teaType}";
                break;
        }

        return $"{compiledString}";
    }
}
