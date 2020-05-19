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

    private List<string> goodPhrase = new List<string> {
        "Wow!",
        "Yum!",
        "Mmmmm.",
    };

    private List<string> mediocrePhrase = new List<string> {
        "Hmmm.",
        "Oh.",
        "Well…",
    };

    private List<string> badPhrase = new List<string> {
        "Uh oh!",
        "Yikes!",
        "Drats.",
        "Bummer…",
    };

    private List<string> goodDescriptor = new List<string> {
        "good",
        "amazing",
        "perfect",
        "so yummy",
        "refreshing",
    };

    private List<string> mediocreDescriptor = new List<string> {
        "okay",
        "edible",
        "pretty good",
        "mediocre",
    };

    private List<string> badDescriptor = new List<string> {
        "bad",
        "weird",
        "upsetting",
        "gross",
    };

    private List<string> goodComment = new List<string> {
        "The flavor is spot on!",
        "This is exactly what I asked for!",
        "You should get a raise!",
        "Thank you so much!",
    };

    // mediocre/bad comments
    // "The flavor is a little off.",
    // "There’s too much (splash flavor)."
    // "There’s not enough (50/50 flavor)."
    // "Where’s the (50/50 flavor) though?"
    // "Do I taste (flavor that’s not supposed to be there)?"



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
        return "Wow! Delicious.";
    }

    public string CompileDrinkName() {
        return "Strawberry honeydew with a splash of coconut bubble tea with light ice";
    }
}
