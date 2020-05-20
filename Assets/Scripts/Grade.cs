﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grade {
    public static Dictionary<Order.AddInOption, float> perfectAddInPercentages = new Dictionary<Order.AddInOption, float> {
        { Order.AddInOption.None,    0.0f },
        { Order.AddInOption.Light,   0.10f },
        { Order.AddInOption.Regular, 0.33f },
        { Order.AddInOption.Extra,   0.5f },
    };

    public static Dictionary<Order.DrinkType, float> perfectDrinkTypePercentages = new Dictionary<Order.DrinkType, float> {
        { Order.DrinkType.Splash, 0.25f },
        { Order.DrinkType.Half,   0.50f },
        { Order.DrinkType.Single, 1.00f },
    };

    private enum CommentType {
        Exclamation,
        Descriptor,
        Phrase,
    }

    public enum LetterGrade {
        A,
        C,
        F
    }
    public LetterGrade letterGrade;
    public string comment;
    public string drinkName;

    public static Grade Compile() {
        Grade grade = new Grade();

        grade.letterGrade = grade.CalculateLetterGrade();
        grade.drinkName = grade.CompileDrinkName();
        grade.comment = grade.CompileComment();

        return grade;
    }

    private Dictionary<LetterGrade, Dictionary<CommentType, List<string>>> InitializeComments() {
        List<string> goodExclamation = new List<string> {
            "Wow!",
            "Yum!",
            "Mmmmm!",
        };

            List<string> mediocreExclamation = new List<string> {
            "Hmmm.",
            "Oh.",
            "Well…",
        };

            List<string> badExclamation = new List<string> {
            "Uh oh!",
            "Yikes!",
            "Drats.",
            "Bummer…",
        };

            List<string> goodDescriptor = new List<string> {
            "great!",
            "amazing!",
            "perfect!",
            "so yummy!",
            "refreshing!",
        };

            List<string> mediocreDescriptor = new List<string> {
            "ok.",
            "able to be drank.",
            "pretty good.",
            "mediocre.",
        };

            List<string> badDescriptor = new List<string> {
            "bad.",
            "weird.",
            "upsetting.",
            "gross.",
        };

        List<string> goodPhrase = new List<string> {
            $"The {Globals.currentOrder.drinkFlavors.First()} flavor is spot on!",
            "This is exactly what I asked for!",
            "The boba amount is just right.",
            "The ice amount is just right.",
            "You should get a raise!",
            "Thank you so much!",
        };

        // TODO: Fill these out
        List<string> mediocrePhrase = new List<string> {
            "The flavor is a little odd.",
            "There’s too much (splash flavor).",
            "There’s not enough (50/50 flavor).",
            "Where’s the (50/50 flavor) though?",
            "Do I taste (flavor that’s not supposed to be there)?",
            "There's too little boba.",
            "There's too much boba.",
        };

        // TODO: Fill these out
        List<string> badPhrase = new List<string> {
            "The flavor is way off.",
            "There’s too much (splash flavor).",
            "There’s not enough (50/50 flavor).",
            "Where’s the (50/50 flavor) though?",
            "Do I taste (flavor that’s not supposed to be there)?",
        };

        return new Dictionary<LetterGrade, Dictionary<CommentType, List<string>>> {
            { LetterGrade.A, new Dictionary<CommentType, List<string>> {
                { CommentType.Exclamation, goodExclamation},
                { CommentType.Descriptor, goodDescriptor },
                { CommentType.Phrase, goodPhrase },
            } },

            { LetterGrade.C, new Dictionary<CommentType, List<string>> {
                { CommentType.Exclamation, mediocreExclamation},
                { CommentType.Descriptor, mediocreDescriptor },
                { CommentType.Phrase, mediocrePhrase },
            } },

            { LetterGrade.F, new Dictionary<CommentType, List<string>> {
                { CommentType.Exclamation, badExclamation },
                { CommentType.Descriptor, badDescriptor },
                { CommentType.Phrase, badPhrase },
            } },
        };
    }

    private LetterGrade CalculateLetterGrade() {
        float score = 1.0f;
        float bobaDeductions = BobaDeductions();
        float iceDeductions = IceDeductions();
        float flavorDeductions = FlavorDeductions();
        LetterGrade grade;

        score = score - bobaDeductions - iceDeductions - flavorDeductions;

        Debug.Log($"score: {score}");
        Debug.Log($"bobaDeductions: {bobaDeductions}");
        Debug.Log($"iceDeductions: {iceDeductions}");
        Debug.Log($"flavorDeductions: {flavorDeductions}");

        if (score >= 0.8f) {
            grade = LetterGrade.A;
        } else if (score < 0.8f && score >= 0.4f) {
            grade = LetterGrade.C;
        } else {
            grade = LetterGrade.F;
        }

        return grade;
    }

    private List<Order.Flavor> ExtraFlavorsAdded() {
        List<Order.Flavor> drinkFlavors = Globals.liquidPercentages.Keys.ToList();
        List<Order.Flavor> orderFlavors = Globals.currentOrder.drinkFlavors;

        IEnumerable<Order.Flavor> extraFlavors = drinkFlavors.Except(orderFlavors);

        return extraFlavors.ToList();
    }

    private float FlavorDeductions() {
        // TODO: 2nd flavor logic

        float deductionWeight = 1.0f;

        // TODO: Check if empty first
        //float percentageOfMainFlavor = Globals.liquidPercentages[Globals.currentOrder.drinkFlavors[0]];
        //float percentageOfSecondFlavor = Globals.liquidPercentages[Globals.currentOrder.drinkFlavors[1]];
        float idealFlavorPercentage = perfectDrinkTypePercentages[Globals.currentOrder.drinkType];

        //float firstFlavorDifference = Math.Abs(percentageOfMainFlavor - idealFlavorPercentage);
        //float secondFlavorDifference = Math.Abs(percentageOfSecondFlavor - idealFlavorPercentage);
        float extraFlavorDifference = PercentageOfExtraFlavorsAdded();

        //return (firstFlavorDifference + secondFlavorDifference + extraFlavorDifference) * deductionWeight;
        //return (firstFlavorDifference + extraFlavorDifference) * deductionWeight;
        return (extraFlavorDifference) * deductionWeight;
    }

    private float PercentageOfExtraFlavorsAdded() {
        List<Order.Flavor> extraFlavors = ExtraFlavorsAdded();
        float percentage = 0.0f;

        foreach (var flavor in extraFlavors) {
            percentage += Globals.liquidPercentages[flavor];
        }

        return percentage;
    }

    private float BobaPercentage() {
        return Globals.bobaCount / Globals.maxBobaCount;
    }

    private float BobaDeductions() {
        float deductionWeight = 1.0f;
        float bobaPercentage = BobaPercentage();
        float idealBobaPercentage = perfectAddInPercentages[Globals.currentOrder.bobaAmount];

        return Math.Abs((bobaPercentage - idealBobaPercentage) * deductionWeight);
    }

    private float IcePercentage() {
        return Globals.iceCount / Globals.maxIceCount;
    }

    private float IceDeductions() {
        float deductionWeight = 1.0f;
        float icePercentage = IcePercentage();
        float idealIcePercentage = perfectAddInPercentages[Globals.currentOrder.iceAmount];

        return Math.Abs((icePercentage - idealIcePercentage) * deductionWeight);
    }

    private string CompileComment() {
        string exclamation = ChooseString(letterGrade, CommentType.Exclamation);
        string descriptor = ChooseString(letterGrade, CommentType.Descriptor);
        string phrase = ChooseString(letterGrade, CommentType.Phrase);

        return $"{exclamation} {descriptor} {phrase}";
    }

    private string ChooseString(LetterGrade letterGrade, CommentType type) {
        List<string> commentList = InitializeComments()[letterGrade][type];
        string randomOption = commentList[UnityEngine.Random.Range(0, commentList.Count - 1)];

        return randomOption;
    }

    private string CompileDrinkName() {
        // This method returns a person's order as a readable sentence

        // For example:
        // Honeydew tea 
        // Honeydew bubble tea
        // Honeydew bubble tea with light ice
        // Honeydew bubble tea with light boba
        // Honeydew bubble tea with no ice and extra boba

        // Honeydew coconut bubble tea
        // Honeydew with a splash of coconut tea

        return $"{CompileFlavorString()}{CompileIceString()}{CompileBobaString()}";
    }

    private string CompileIceString() {
        string compiledString;
        string iceAmountStr = Globals.currentOrder.iceAmount.ToString().ToLower();

        switch (Globals.currentOrder.iceAmount) {
            case Order.AddInOption.None:
                // Honeydew bubble tea with no ice
                compiledString = " with no ice";
                break;
            case Order.AddInOption.Regular:
                // Honeydew bubble tea
                compiledString = "";
                break;
            default:
                // Honeydew bubble tea with light ice
                compiledString = $" with {iceAmountStr} ice";
                break;
        }

        return compiledString;
    }

    private string CompileBobaString() {
        string compiledString;
        string bobaAmountStr = Globals.currentOrder.bobaAmount.ToString().ToLower();

        if (Globals.currentOrder.bobaAmount == Order.AddInOption.None || Globals.currentOrder.bobaAmount == Order.AddInOption.Regular) {
            // Honeydew tea
            compiledString = "";
        } else if (Globals.currentOrder.iceAmount == Order.AddInOption.Regular) {
            // Honeydew bubble tea with extra boba
            compiledString = $" with {bobaAmountStr} boba";
        } else {
            // Honeydew bubble tea with no ice and light boba
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
            case Order.DrinkType.Splash:
                // Honeydew with a splash of coconut tea
                compiledString = $"{firstFlavor} with a splash of {secondFlavorStr} {teaType}";
                break;
            case Order.DrinkType.Half:
                // Honeydew coconut tea
                compiledString = $"{firstFlavor} {secondFlavorStr} {teaType}";
                break;
            default:
                // Honeydew tea
                compiledString = $"{firstFlavor} {teaType}";
                break;
        }

        return $"{compiledString}";
    }
}
