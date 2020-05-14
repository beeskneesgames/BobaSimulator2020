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
    public bool isBubbleTea;

    public Order() {
        iceAmount = GetRandomOption();
        bobaAmount = GetRandomOption();
        drinkType = GetRandomDrinkType();
        drinkFlavors = GetRandomDrinkFlavors();
        isBubbleTea = bobaAmount != AddInOption.None;
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
        int firstIndex = UnityEngine.Random.Range(0, flavorOptions.Length - 1);
        int secondIndex;

        Flavor firstFlavor = (Flavor)flavorOptions.GetValue(firstIndex);

        flavors.Add(firstFlavor);

        if (numberOfFlavors == 2) {
            secondIndex = UnityEngine.Random.Range(0, flavorOptions.Length - 1);

            while (firstIndex == secondIndex) {
                secondIndex = UnityEngine.Random.Range(0, flavorOptions.Length - 1);
            }

            Flavor secondFlavor = (Flavor)flavorOptions.GetValue(secondIndex);
            flavors.Add(secondFlavor);
        }

        return flavors;
    }

    private string CompileIceString() {
        string compiledString;

        if (iceAmount == AddInOption.Regular) {
            // Mango bubble tea
            compiledString = "";
        } else {
            // Mango bubble tea with light ice
            compiledString = $" with {iceAmount}";
        }

        return compiledString;
    }

    private string CompileBobaString() {
        string compiledString;

        if (bobaAmount == AddInOption.None || bobaAmount == AddInOption.Regular) {
            // Mango tea
            compiledString = "";
        } else if (iceAmount == AddInOption.Regular) {
            // Mango bubble tea with extra boba
            compiledString = $" with {bobaAmount}";
        } else {
            // Mango bubble tea with no ice and light boba
            compiledString = $" and {bobaAmount}";
        }

        return compiledString;
    }

    private string CompileFlavorString() {
        Flavor firstFlavor = drinkFlavors.First();
        Flavor secondFlavor = drinkFlavors.Last();
        string teaType = isBubbleTea ? "bubble tea" : "tea";

        string compiledString;

        switch (drinkType) {
            case FlavorOption.Splash:
                compiledString = $"{firstFlavor} {teaType} with a splash of ${secondFlavor}";
                break;
            case FlavorOption.Half:
                compiledString = $"{firstFlavor} ${secondFlavor} {teaType}";
                break;
            default:
                compiledString = $"{firstFlavor} {teaType}";
                break;
        }

        return $"{compiledString}";
    }
}
