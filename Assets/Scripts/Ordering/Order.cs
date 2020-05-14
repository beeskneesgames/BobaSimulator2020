using System;
using System.Collections.Generic;
using System.Linq;

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

        // Mango Blueberry bubble tea
        // Mango with a splash of Blueberry tea

        return $"{CompileFlavorString()}{CompileIceString()}{CompileBobaString()}";
    }

    private string CompileIceString() {
        string compiledString;
        string iceAmountStr = iceAmount.ToString().ToLower();

        if (iceAmount == AddInOption.Regular) {
            // Mango bubble tea
            compiledString = "";
        } else {
            // Mango bubble tea with light ice
            compiledString = $" with {iceAmountStr} ice";
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
        string teaType = IsBubbleTea() ? "bubble tea" : "tea";

        string compiledString;

        switch (drinkType) {
            case FlavorOption.Splash:
                compiledString = $"{firstFlavor} with a splash of {secondFlavor} {teaType}";
                break;
            case FlavorOption.Half:
                compiledString = $"{firstFlavor} {secondFlavor} {teaType}";
                break;
            default:
                compiledString = $"{firstFlavor} {teaType}";
                break;
        }

        return $"{compiledString}";
    }
}
