﻿using System.Collections.Generic;
using System.Linq;

public class Grade {
    private Dictionary<Order.AddInOption, float> perfectAddInPercentages = new Dictionary<Order.AddInOption, float> {
        { Order.AddInOption.None,    0.0f },
        { Order.AddInOption.Light,   0.10f },
        { Order.AddInOption.Regular, 0.33f },
        { Order.AddInOption.Extra,   0.5f },
    };

    private Dictionary<Order.FlavorOption, float> perfectFlavorOptionPercentages = new Dictionary<Order.FlavorOption, float> {
        { Order.FlavorOption.Splash, 0.25f },
        { Order.FlavorOption.Half,   0.50f },
        { Order.FlavorOption.Single, 1.00f },
    };

    private static List<string> goodExclamation = new List<string> {
        "Wow!",
        "Yum!",
        "Mmmmm!",
    };

    private static List<string> mediocreExclamation = new List<string> {
        "Hmmm.",
        "Oh.",
        "Well…",
    };

    private static List<string> badExclamation = new List<string> {
        "Uh oh!",
        "Yikes!",
        "Drats.",
        "Bummer…",
    };

    private static List<string> goodDescriptor = new List<string> {
        "great!",
        "amazing!",
        "perfect!",
        "so yummy!",
        "refreshing!",
    };

    private static List<string> mediocreDescriptor = new List<string> {
        "ok.",
        "able to be drank.",
        "pretty good",
        "mediocre",
    };

    private static List<string> badDescriptor = new List<string> {
        "bad",
        "weird",
        "upsetting",
        "gross",
    };

    private static List<string> goodPhrase = new List<string> {
        "The flavor is spot on!",
        "This is exactly what I asked for!",
        "You should get a raise!",
        "Thank you so much!",
    };


    private static Dictionary<string, Dictionary<string, List<string>>> comments = new Dictionary<string, Dictionary<string, List<string>>> {
        { "good", new Dictionary<string, List<string>> {
            { "exclamation", goodExclamation },
            { "descriptor", goodDescriptor },
        } },

        { "mediocre", new Dictionary<string, List<string>> {
            { "exclamation", mediocreExclamation},
            { "descriptor", mediocreDescriptor },
        } },

        { "bad", new Dictionary<string, List<string>> {
            { "exclamation", badExclamation },
            { "descriptor", badDescriptor },
        } },
    };

    public enum LetterGrade {
        A,
        C,
        F
    }
    public LetterGrade letterGrade;
    public string comment;
    public string drinkName;

    public Grade Compile() {
        LetterGrade letterGrade = CompileLetterGrade();

        Grade grade = new Grade {
            letterGrade = letterGrade,
            drinkName = CompileDrinkName(),
            comment = CompileComment(),
        };

        return grade;
    }

    private LetterGrade CompileLetterGrade() {
        return LetterGrade.A;
    }

    private List<Order.Flavor> ExtraFlavorsAdded() {
        return Globals.currentOrder.drinkFlavors;
    }

    private float BobaPercentage() {
        return Globals.bobaCount / Globals.maxBobaCount;
    }

    private float IcePercentage() {
        return Globals.iceCount / Globals.maxIceCount;
    }

    public string CompileComment() {
        // mediocre/bad phrase
        // "The flavor is a little off.",
        // "There’s too much (splash flavor)."
        // "There’s not enough (50/50 flavor)."
        // "Where’s the (50/50 flavor) though?"
        // "Do I taste (flavor that’s not supposed to be there)?"

        //[exclamation]! This tea was [descriptor]. [phrase].
        string exclamation;
        string descriptor;
        string phrase;
        string comment;

        switch (letterGrade) {
            case LetterGrade.A:
                exclamation = ChooseComment("good", "exclamation");
                descriptor = ChooseComment("good", "descriptor");
                phrase = "";
                break;
            case LetterGrade.C:
                exclamation = ChooseComment("mediocre", "exclamation");
                descriptor = ChooseComment("good", "descriptor");
                phrase = "";
                break;
            case LetterGrade.F:
                exclamation = ChooseComment("bad", "exclamation");
                descriptor = ChooseComment("good", "descriptor");
                phrase = "";
                break;
            default:
                exclamation = "";
                descriptor = "";
                phrase = "";
                break;
        }

        comment = $"{exclamation} {descriptor} {phrase}";

        return comment;
    }

    private string ChooseComment(string level, string type) {
        List<string> commentList = comments[level][type];
        string randomOption = comments[level][type][UnityEngine.Random.Range(1, commentList.Count - 1)];
        return randomOption;
    }

    public string CompileDrinkName() {
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
        string iceAmountStr = Globals.currentOrder.iceAmount.ToString().ToLower();

        switch (Globals.currentOrder.iceAmount) {
            case Order.AddInOption.None:
                // Mango bubble tea with no ice
                compiledString = " with no ice";
                break;
            case Order.AddInOption.Regular:
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
        string bobaAmountStr = Globals.currentOrder.bobaAmount.ToString().ToLower();

        if (Globals.currentOrder.bobaAmount == Order.AddInOption.None || Globals.currentOrder.bobaAmount == Order.AddInOption.Regular) {
            // Mango tea
            compiledString = "";
        } else if (Globals.currentOrder.iceAmount == Order.AddInOption.Regular) {
            // Mango bubble tea with extra boba
            compiledString = $" with {bobaAmountStr} boba";
        } else {
            // Mango bubble tea with no ice and light boba
            compiledString = $" and {bobaAmountStr} boba";
        }

        return compiledString;
    }

    private string CompileFlavorString() {
        Order.Flavor firstFlavor = Globals.currentOrder.drinkFlavors.First();
        Order.Flavor secondFlavor = Globals.currentOrder.drinkFlavors.Last();
        string secondFlavorStr = secondFlavor.ToString().ToLower();
        string teaType = Globals.currentOrder.IsBubbleTea() ? "bubble tea" : "tea";

        string compiledString;

        switch (Globals.currentOrder.drinkType) {
            case Order.FlavorOption.Splash:
                // Mango with a splash of lemon tea
                compiledString = $"{firstFlavor} with a splash of {secondFlavorStr} {teaType}";
                break;
            case Order.FlavorOption.Half:
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
