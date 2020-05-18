using System;
using System.Collections.Generic;
using System.Linq;

public class Order {
    public enum Flavor {
        NotSet,
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
        NotSet,
        Splash,
        Half,
        Single,
    };

    public enum AddInOption {
        NotSet,
        None,
        Light,
        Regular,
        Extra,
    };

    public AddInOption iceAmount = AddInOption.NotSet;
    public AddInOption bobaAmount = AddInOption.NotSet;
    public FlavorOption drinkType = FlavorOption.NotSet;
    public List<Flavor> drinkFlavors = new List<Flavor> { Flavor.NotSet };

    public bool IsBubbleTea() {
        bool isBubbleTea = bobaAmount != AddInOption.None && bobaAmount != AddInOption.NotSet;

        return isBubbleTea;
    }

    public bool IsValid() {
        bool isValid = true;
        bool multipleFlavors = drinkFlavors.Count == 2;

        if (iceAmount == AddInOption.NotSet ||
            bobaAmount == AddInOption.NotSet ||
            drinkType == FlavorOption.NotSet ||
            drinkFlavors.First() == Flavor.NotSet ||
            drinkFlavors.Last() == Flavor.NotSet ||
            (multipleFlavors && drinkFlavors.First() == drinkFlavors.Last())) {
            isValid = false;
        }
        return isValid;
    }

    public static Order GenerateBasic() {
        FlavorOption drinkType = FlavorOption.Single;

        Order randomOrder = new Order {
            iceAmount = AddInOption.Regular,
            bobaAmount = AddInOption.Regular,
            drinkType = drinkType,
            drinkFlavors = GetRandomDrinkFlavors(drinkType),
        };

        return randomOrder;
    }

    public static Order GenerateBasic(List<Flavor> flavors) {
        FlavorOption drinkType = FlavorOption.Single;

        Order randomOrder = new Order {
            iceAmount = AddInOption.Regular,
            bobaAmount = AddInOption.Regular,
            drinkType = drinkType,
            drinkFlavors = flavors,
        };

        return randomOrder;
    }

    public static Order GenerateRandom() {
        FlavorOption drinkType = GetRandomDrinkType();

        Order randomOrder = new Order {
            iceAmount = GetRandomOption(),
            bobaAmount = GetRandomOption(),
            drinkType = drinkType,
            drinkFlavors = GetRandomDrinkFlavors(drinkType),
        };

        return randomOrder;
    }

    private static AddInOption GetRandomOption() {
        Array addInOptions = Enum.GetValues(typeof(AddInOption));

        // Start at 1 to avoid setting AddInOption.NotSet
        AddInOption randomOption = (AddInOption)addInOptions.GetValue(
            UnityEngine.Random.Range(1, addInOptions.Length - 1)
        );

        return randomOption;
    }

    private static FlavorOption GetRandomDrinkType() {
        Array liquidOptions = Enum.GetValues(typeof(FlavorOption));

        // Start at 1 to avoid setting FlavorOption.NotSet
        FlavorOption randomOption = (FlavorOption)liquidOptions.GetValue(
            UnityEngine.Random.Range(1, liquidOptions.Length - 1)
        );

        return randomOption;
    }

    private static List<Flavor> GetRandomDrinkFlavors(FlavorOption drinkType) {
        int numberOfFlavors = (drinkType == FlavorOption.Single) ? 1 : 2;
        List<Flavor> flavors = new List<Flavor> { };
        Array flavorOptions = Enum.GetValues(typeof(Flavor));

        // Start at 1 to avoid setting Flavor.NotSet
        int firstIndex = UnityEngine.Random.Range(1, flavorOptions.Length - 1);

        Flavor firstFlavor = (Flavor)flavorOptions.GetValue(firstIndex);

        flavors.Add(firstFlavor);

        if (numberOfFlavors == 2) {
            int secondIndex;

            do {
                // Start at 1 to avoid setting Flavor.NotSet
                secondIndex = UnityEngine.Random.Range(1, flavorOptions.Length - 1);
            } while (firstIndex == secondIndex);

            Flavor secondFlavor = (Flavor)flavorOptions.GetValue(secondIndex);
            flavors.Add(secondFlavor);
        }

        return flavors;
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
