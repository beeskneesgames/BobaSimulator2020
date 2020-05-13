using System.Collections;
using System.Collections.Generic;

public class Order {
    static List<string> flavors = new List<string> {
        "Blueberry",
        "Classic Milk Tea",
        "Coconut",
        "Honeydew",
        "Mango",
        "Matcha",
        "Strawberry",
        "Taro",
        "Thai Tea",
    };

    static List<string> liquidOptions = new List<string> {
        "Splash",
        "50/50",
        "Single flavor",
    };

    static List<string> addInOptions = new List<string> {
        "No",
        "Light",
        "Regular",
        "Extra",
    };

    public string Compile() {
        return $"{CompileIce()}{CompileBoba()}{CompileFlavor()}";
    }

    private string CompileIce() {
        return CompileAddIns("ice");
    }

    private string CompileBoba() {
        return CompileAddIns("boba");
    }

    private string CompileFlavor() {
        return "Classic Milk Tea with a splash of Coconut";
    }

    private string CompileAddIns(string addIn) {
        string addInOrder = "";
        string chosenOption = addInOptions[UnityEngine.Random.Range(0, addInOptions.Count - 1)];

        if (chosenOption == "Regular") {
            return addInOrder;
        };

        return $"{addInOrder} {addIn} ";
    }
}
