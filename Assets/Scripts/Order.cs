using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public string IceDescription {
        get {
            switch (iceAmount) {
                case AddInOption.None:
                    return "No ice";
                case AddInOption.Light:
                    return "Light ice";
                case AddInOption.Regular:
                    return "Regular ice";
                case AddInOption.Extra:
                    return "Extra ice";
                default:
                    Debug.LogError("Invalid ice amount");
                    return "Invalid ice";
            }
        }
    }

    public string BobaDescription {
        get {
            switch (bobaAmount) {
                case AddInOption.None:
                    return "No boba";
                case AddInOption.Light:
                    return "Light boba";
                case AddInOption.Regular:
                    return "Regular boba";
                case AddInOption.Extra:
                    return "Extra boba";
                default:
                    Debug.LogError("Invalid boba amount");
                    return "Invalid boba";
            }
        }
    }

    public string FlavorDescription {
        get {
            switch (drinkType) {
                case FlavorOption.Single:
                    return drinkFlavors[0].ToString();
                case FlavorOption.Half:
                    return $"{drinkFlavors[0]} {drinkFlavors[1].ToString().ToLower()}";
                case FlavorOption.Splash:
                    return $"{drinkFlavors[0]} with a splash of {drinkFlavors[1].ToString().ToLower()}";
                default:
                    Debug.LogError("Invalid flavor option ");
                    return "Invalid flavor";
            }
        }
    }
}
