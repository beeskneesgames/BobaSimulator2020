using System;
using System.Collections.Generic;

public class OrderGenerator {
    public static Order GenerateBasicOrder() {
        Order.FlavorOption drinkType = Order.FlavorOption.Single;

        Order randomOrder = new Order {
            iceAmount = Order.AddInOption.Regular,
            bobaAmount = Order.AddInOption.Regular,
            drinkType = drinkType,
            drinkFlavors = GetRandomDrinkFlavors(drinkType),
        };

        return randomOrder;
    }

    public static Order GenerateBasicOrder(List<Order.Flavor> flavors) {
        Order.FlavorOption drinkType = Order.FlavorOption.Single;

        Order randomOrder = new Order {
            iceAmount = Order.AddInOption.Regular,
            bobaAmount = Order.AddInOption.Regular,
            drinkType = drinkType,
            drinkFlavors = flavors,
        };

        return randomOrder;
    }

    public static Order GenerateRandomOrder() {
        Order.FlavorOption drinkType = GetRandomDrinkType();

        Order randomOrder = new Order {
            iceAmount = GetRandomOption(),
            bobaAmount = GetRandomOption(),
            drinkType = drinkType,
            drinkFlavors = GetRandomDrinkFlavors(drinkType),
        };

        return randomOrder;
    }

    private static Order.AddInOption GetRandomOption() {
        Array addInOptions = Enum.GetValues(typeof(Order.AddInOption));

        // Start at 1 to avoid setting Order.FlavorOption.NotSet
        Order.AddInOption randomOption = (Order.AddInOption)addInOptions.GetValue(
            UnityEngine.Random.Range(1, addInOptions.Length - 1)
        );

        return randomOption;
    }

    private static Order.FlavorOption GetRandomDrinkType() {
        Array liquidOptions = Enum.GetValues(typeof(Order.FlavorOption));

        // Start at 1 to avoid setting Order.FlavorOption.NotSet
        Order.FlavorOption randomOption = (Order.FlavorOption)liquidOptions.GetValue(
            UnityEngine.Random.Range(1, liquidOptions.Length - 1)
        );

        return randomOption;
    }

    private static List<Order.Flavor> GetRandomDrinkFlavors(Order.FlavorOption drinkType) {
        int numberOfFlavors = (drinkType == Order.FlavorOption.Single) ? 1 : 2;
        List<Order.Flavor> flavors = new List<Order.Flavor> { };
        Array flavorOptions = Enum.GetValues(typeof(Order.Flavor));

        // Start at 1 to avoid setting Order.FlavorOption.NotSet
        int firstIndex = UnityEngine.Random.Range(1, flavorOptions.Length - 1);

        Order.Flavor firstFlavor = (Order.Flavor)flavorOptions.GetValue(firstIndex);

        flavors.Add(firstFlavor);

        if (numberOfFlavors == 2) {
            int secondIndex;

            do {
                // Start at 1 to avoid setting Order.FlavorOption.NotSet
                secondIndex = UnityEngine.Random.Range(1, flavorOptions.Length - 1);
            } while (firstIndex == secondIndex);

            Order.Flavor secondFlavor = (Order.Flavor)flavorOptions.GetValue(secondIndex);
            flavors.Add(secondFlavor);
        }

        return flavors;
    }
}
