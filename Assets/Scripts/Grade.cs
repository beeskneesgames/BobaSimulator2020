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
            "Bummer…",
            "Errrr…",
        };
        List<string> goodDescriptor = new List<string> {
            "great!",
            "amazing!",
            "perfect!",
            "so yummy!",
            "refreshing!",
            "delicious.",
        };
        List<string> mediocreDescriptor = new List<string> {
            "ok.",
            "drinkable.",
            "mediocre.",
            "slightly odd.",
            "interesting.",
        };
        List<string> badDescriptor = new List<string> {
            "bad.",
            "weird.",
            "upsetting.",
            "gross.",
            "isn't right.",
        };

        List<string> goodPhrase = new List<string> {
            $"The {Globals.currentOrder.drinkFlavors.First()} flavor is spot on!",
            "This is exactly what I asked for!",
            "You should get a raise!",
            "Thank you so much!",
            "Yum yum yum yum.",
        };

        List<string> mediocrePhrase = new List<string> {
            "The flavor is a little odd.",
            "It's not quite what I was expecting.",
        };

        List<string> badPhrase = new List<string> {
            "The flavor is way off.",
            "I'd like a remake please.",
            "Is this even my order?",
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

        Debug.Log($"boba deductions: {bobaDeductions}");
        Debug.Log($"ice deductions: {iceDeductions}");
        Debug.Log($"flavor deductions: {flavorDeductions}");

        score = score - bobaDeductions - iceDeductions - flavorDeductions;

        if (score >= 0.88f) {
            grade = LetterGrade.A;
        } else if (score >= 0.8f) {
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
        float deductionWeight = 0.4f;
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

        return ((firstFlavorDifference + secondFlavorDifference) * deductionWeight) + extraFlavorDifference;
    }

    private float PercentageOfExtraFlavorsAdded() {
        float deductionWeight = 0.1f;
        List<Order.Flavor> extraFlavors = ExtraFlavorsAdded();
        float percentage = 0.0f;

        foreach (var flavor in extraFlavors) {
            percentage += Globals.liquidPercentages[flavor];
        }

        return percentage * deductionWeight;
    }

    private float BobaPercentage() {
        return (float)Globals.bobaCount / Globals.maxBobaCount;
    }

    private float BobaDeductions() {
        float deductionWeight = 0.5f;

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
        float deductionWeight = 0.1f;

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
        string phrase = ChoosePhrase(letterGrade);

        return $"{exclamation} This tea is {descriptor} {phrase}";
    }

    private string ChooseString(LetterGrade letterGrade, CommentType type) {
        List<string> commentList = InitializeComments()[letterGrade][type];
        string randomOption = commentList[UnityEngine.Random.Range(0, commentList.Count)];

        return randomOption;
    }

    private string ChoosePhrase(LetterGrade letterGrade) {
        List<string> commentList = InitializeComments()[letterGrade][CommentType.Phrase];
        float bobaDifference = BobaDifference();
        float iceDifference = IceDifference();
        float flavorDeductions = FlavorDeductions();
        List<Order.Flavor> extraFlavors = ExtraFlavorsAdded();
        string phrase = "";

        if (letterGrade == LetterGrade.A) {
            if (bobaDifference <= 0.01f && bobaDifference >= -0.01f) {
                phrase = "The boba amount is just right.";
            }
        } else if (letterGrade == LetterGrade.C || letterGrade == LetterGrade.F) {
            if (extraFlavors.Count >= 1 && flavorDeductions >= 0.2f) {
                phrase = "This flavor is really really weird.";
            }
            if (Globals.currentOrder.bobaAmount == Order.AddInOption.None) {
                if (bobaDifference >= 0.01f) {
                    phrase = "Why is there boba in here?";
                }
            } else if (Globals.bobaCount == 0) {
                phrase = "Where is the boba?";
            } else if (Globals.currentOrder.iceAmount == Order.AddInOption.None) {
                if (iceDifference >= 0.01f) {
                    phrase = "Why is there ice in here?";
                }
            } else if (Globals.iceCount == 0) {
                phrase = "Where is the ice?";
            } else if (bobaDifference > 0.10f) {
                phrase = "There's way too much boba.";
            } else if (bobaDifference >= 0.05f) {
                phrase = "There's too much boba.";
            } else if (bobaDifference <= -0.15f) {
                phrase = "Where's the boba?";
            } else if (bobaDifference <= -0.10f) {
                phrase = "There's too little boba.";
            } else if (extraFlavors.Count > 0) {
                phrase = $"Do I taste {extraFlavors[0]}?";
            } else if (iceDifference >= 0.10f) {
                phrase = "There's way too much ice.";
            } else if (iceDifference >= 0.05f) {
                phrase = "There's too much ice.";
            } else if (iceDifference <= -0.15f) {
                phrase = "Where's the ice?";
            } else if (iceDifference <= -0.10f) {
                phrase = "There's too little ice.";
            }
        }

        if (String.IsNullOrEmpty(phrase)) {
            phrase = ChooseString(letterGrade, CommentType.Phrase);
}

        return phrase;
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
