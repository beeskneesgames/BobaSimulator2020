using System.Collections;
using System.Collections.Generic;

public class Grade {
    private Order currentOrder = Globals.currentOrder;
    private int bobaCount = Globals.bobaCount;
    private int iceCount = Globals.iceCount;

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

    public enum LetterGrade {
        A,
        C,
        F
    }

    public Grade Compile() {
        LetterGrade letterGrade = CompileLetterGrade();

        Grade grade = new Grade();

        return grade;
    }

    private LetterGrade CompileLetterGrade() {
        return LetterGrade.A;
    }

    private List<Order.Flavor> ExtraFlavorsAdded() {
        return new List<Order.Flavor>();
    }
}
