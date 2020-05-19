using System.Collections;
using System.Collections.Generic;

public class Grade {
    private readonly Order currentOrder = Globals.currentOrder;
    private readonly int bobaCount = Globals.bobaCount;
    private readonly int iceCount = Globals.iceCount;

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
        return currentOrder.drinkFlavors;
    }

    private float BobaPercentage() {
        return bobaCount / 100;
    }

    private float IcePercentage() {
        return iceCount / 100;
    }

    public string CompileComment() {
        // mediocre/bad phrase
        // "The flavor is a little off.",
        // "There’s too much (splash flavor)."
        // "There’s not enough (50/50 flavor)."
        // "Where’s the (50/50 flavor) though?"
        // "Do I taste (flavor that’s not supposed to be there)?"

        //[exclamation]! This tea was [descriptor]. [phrase].
        string comment;

        switch (letterGrade) {
            case LetterGrade.A:
                comment = "yum";
                break;
            case LetterGrade.C:
                comment = "yum";
                break;
            case LetterGrade.F:
                comment = "yum";
                break;
            default:
                comment = "yum";
                break;
        }

        return comment;
    }

    public string CompileDrinkName() {
        return "Strawberry honeydew with a splash of coconut bubble tea with light ice";
    }
}
