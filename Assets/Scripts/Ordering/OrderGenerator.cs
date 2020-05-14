﻿using System;
using System.Collections.Generic;
using System.Linq;

public class OrderGenerator {
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

    // TODO: These two random methods are the same but the types are diff
    //       Is there a way to combine without it being messy?
    public static Order.AddInOption GetRandomOption() {
        Array addInOptions = Enum.GetValues(typeof(Order.AddInOption));
        Order.AddInOption randomOption = (Order.AddInOption)addInOptions.GetValue(
            UnityEngine.Random.Range(0, addInOptions.Length - 1)
        );

        return randomOption;
    }

    public static Order.FlavorOption GetRandomDrinkType() {
        Array liquidOptions = Enum.GetValues(typeof(Order.FlavorOption));
        Order.FlavorOption randomOption = (Order.FlavorOption)liquidOptions.GetValue(
            UnityEngine.Random.Range(0, liquidOptions.Length - 1)
        );

        return randomOption;
    }

    public static List<Order.Flavor> GetRandomDrinkFlavors(Order.FlavorOption drinkType) {
        int numberOfFlavors = (drinkType == Order.FlavorOption.Single) ? 1 : 2;
        List<Order.Flavor> flavors = new List<Order.Flavor> { };
        Array flavorOptions = Enum.GetValues(typeof(Order.Flavor));
        int firstIndex = UnityEngine.Random.Range(0, flavorOptions.Length - 1);
        int secondIndex;

        Order.Flavor firstFlavor = (Order.Flavor)flavorOptions.GetValue(firstIndex);

        flavors.Add(firstFlavor);

        if (numberOfFlavors == 2) {
            do {
                secondIndex = UnityEngine.Random.Range(0, flavorOptions.Length - 1);
            } while (firstIndex == secondIndex);

            Order.Flavor secondFlavor = (Order.Flavor)flavorOptions.GetValue(secondIndex);
            flavors.Add(secondFlavor);
        }

        return flavors;
    }
}
