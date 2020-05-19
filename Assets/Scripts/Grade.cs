using System;
using System.Collections.Generic;
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
        "pretty good.",
        "mediocre.",
    };

    private static List<string> badDescriptor = new List<string> {
        "bad.",
        "weird.",
        "upsetting.",
        "gross.",
    };

    private static List<string> goodPhrase = new List<string> {
        $"The {Globals.currentOrder.drinkFlavors.First()} flavor is spot on!",
        "This is exactly what I asked for!",
        "The boba amount is just right.",
        "The ice amount is just right.",
        "You should get a raise!",
        "Thank you so much!",
    };

    // TODO: Fill these out
    private static List<string> mediocrePhrase = new List<string> {
        "The flavor is a little odd.",
        "There’s too much (splash flavor).",
        "There’s not enough (50/50 flavor).",
        "Where’s the (50/50 flavor) though?",
        "Do I taste (flavor that’s not supposed to be there)?",
        "There's too little boba.",
        "There's too much boba.",
    };

    // TODO: Fill these out
    private static List<string> badPhrase = new List<string> {
        "The flavor is way off.",
        "There’s too much (splash flavor).",
        "There’s not enough (50/50 flavor).",
        "Where’s the (50/50 flavor) though?",
        "Do I taste (flavor that’s not supposed to be there)?",
    };

    private static Dictionary<LetterGrade, Dictionary<CommentType, List<string>>> comments = new Dictionary<LetterGrade, Dictionary<CommentType, List<string>>> {
        { LetterGrade.A, new Dictionary<CommentType, List<string>> {
            { CommentType.exclamation, goodExclamation },
            { CommentType.descriptor, goodDescriptor },
            { CommentType.phrase, goodPhrase },
        } },

        { LetterGrade.C, new Dictionary<CommentType, List<string>> {
            { CommentType.exclamation, mediocreExclamation},
            { CommentType.descriptor, mediocreDescriptor },
            { CommentType.phrase, mediocrePhrase },
        } },

        { LetterGrade.F, new Dictionary<CommentType, List<string>> {
            { CommentType.exclamation, badExclamation },
            { CommentType.descriptor, badDescriptor },
            { CommentType.phrase, badPhrase },
        } },
    };

    private enum CommentType {
        exclamation,
        descriptor,
        phrase,
    }

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
        float overallGrade = 1.0f;
        float bobaGrade = GradeBoba();
        float iceGrade = GradeIce();

        overallGrade = overallGrade - bobaGrade - iceGrade;

        switch (overallGrade) {
            case float gradePercentage when (gradePercentage >= 1.0f):
                return LetterGrade.A;
            case float gradePercentage when (gradePercentage < 1.0f && gradePercentage >= 0.7f):
                return LetterGrade.C;
            default:
                return LetterGrade.F;
        }
    }

    private List<Order.Flavor> ExtraFlavorsAdded() {
        // TODO: Implement this
        return Globals.currentOrder.drinkFlavors;
    }

    private float BobaPercentage() {
        return Globals.bobaCount / Globals.maxBobaCount;
    }

    private float GradeBoba() {
        float bobaPercentage = BobaPercentage();
        float idealBobaPercentage = perfectAddInPercentages[Globals.currentOrder.bobaAmount];

        return Math.Abs(bobaPercentage - idealBobaPercentage);
    }

    private float IcePercentage() {
        return Globals.iceCount / Globals.maxIceCount;
    }

    private float GradeIce() {
        float icePercentage = IcePercentage();
        float idealIcePercentage = perfectAddInPercentages[Globals.currentOrder.iceAmount];

        return Math.Abs(icePercentage - idealIcePercentage);
    }

    public string CompileComment() {
        string exclamation = ChooseString(letterGrade, CommentType.exclamation);
        string descriptor = ChooseString(letterGrade, CommentType.descriptor);
        string phrase = ChooseString(letterGrade, CommentType.phrase);

        return $"{exclamation} {descriptor} {phrase}";
    }

    private string ChooseString(LetterGrade letterGrade, CommentType type) {
        List<string> commentList = comments[letterGrade][type];
        string randomOption = comments[letterGrade][type][UnityEngine.Random.Range(1, commentList.Count - 1)];

        return randomOption;
    }

    public string CompileDrinkName() {
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
            case Order.FlavorOption.Splash:
                // Honeydew with a splash of coconut tea
                compiledString = $"{firstFlavor} with a splash of {secondFlavorStr} {teaType}";
                break;
            case Order.FlavorOption.Half:
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
