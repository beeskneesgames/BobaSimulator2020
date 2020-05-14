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
