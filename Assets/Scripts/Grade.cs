using System;
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
        { Order.DrinkType.Half,   0.50f },
        { Order.DrinkType.Splash, 0.75f },
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
        float bobaDifference = BobaDifference();
        float iceDifference = IceDifference();
        List<Order.Flavor> extraFlavors = ExtraFlavorsAdded();

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

        List<string> mediocrePhrase = new List<string> {
            "The flavor is a little odd.",
            "It's not quite what I was expecting.",
        };

            List<string> badPhrase = new List<string> {
            "The flavor is way off.",
            "I'd like a remake please.",
        };

        if (bobaDifference > 0.2f) {
            mediocrePhrase.Add("There's too much boba.");
            badPhrase.Add("There's way too much boba.");
        } else if (bobaDifference < -0.2f) {
            mediocrePhrase.Add("There's too little boba.");
            badPhrase.Add("Where's the boba?");
        }

        if (iceDifference > 0.2f) {
            mediocrePhrase.Add("There's too much ice.");
            badPhrase.Add("There's way too much ice.");
        } else if (iceDifference < -0.2f) {
            mediocrePhrase.Add("There's too little ice.");
            badPhrase.Add("Where's the ice?");
        }

        if (extraFlavors.Count > 0) {
            mediocrePhrase.Add($"Do I taste {extraFlavors[0]}?");
            badPhrase.Add($"Why do I taste {extraFlavors[0]}?");
        }

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
        } else if (score < 0.8f && score >= 0.6f) {
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
        float deductionWeight = 0.1f;
        float secondFlavorDifference = 0.0f;
        float idealMainFlavorPercentage = perfectDrinkTypePercentages[Globals.currentOrder.drinkType];
        float percentageOfMainFlavor = Globals.liquidPercentages[Globals.currentOrder.drinkFlavors[0]];

        if (Globals.currentOrder.drinkType == Order.DrinkType.Half || Globals.currentOrder.drinkType == Order.DrinkType.Splash) {
            float idealSecondFlavorPercentage = 1.0f - idealMainFlavorPercentage;
            float percentageOfSecondFlavor = Globals.liquidPercentages[Globals.currentOrder.drinkFlavors[1]];
            secondFlavorDifference = Math.Abs(percentageOfSecondFlavor - idealSecondFlavorPercentage);
        }

        float firstFlavorDifference = Math.Abs(percentageOfMainFlavor - idealMainFlavorPercentage);
        float extraFlavorDifference = PercentageOfExtraFlavorsAdded();

        Debug.Log($"firstFlavorDifference: {firstFlavorDifference}");
        Debug.Log($"secondFlavorDifference: {secondFlavorDifference}");
        Debug.Log($"extraFlavorDifference: {extraFlavorDifference}");

        return (firstFlavorDifference + secondFlavorDifference + extraFlavorDifference) * deductionWeight;
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
        return (float)Globals.bobaCount / Globals.maxBobaCount;
    }

    private float BobaDeductions() {
        float deductionWeight = 0.25f;

        return Math.Abs(BobaDifference() * deductionWeight);
    }

    private float BobaDifference() {
        float bobaPercentage = BobaPercentage();
        float idealBobaPercentage = perfectAddInPercentages[Globals.currentOrder.bobaAmount];

        return bobaPercentage - idealBobaPercentage;
    }

    private float IcePercentage() {
        return (float)Globals.iceCount / Globals.maxIceCount;
    }

    private float IceDeductions() {
        float deductionWeight = 0.25f;

        return Math.Abs(IceDifference() * deductionWeight);
    }

    private float IceDifference() {
        float icePercentage = IcePercentage();
        float idealIcePercentage = perfectAddInPercentages[Globals.currentOrder.iceAmount];

        return icePercentage - idealIcePercentage;
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
