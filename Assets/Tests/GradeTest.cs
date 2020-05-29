using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class GradeTest {
        [Test]
        public void CalculateAGradeSingleFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount * Grade.perfectAddInPercentages[order.bobaAmount]);
            Globals.iceCount = (int)(Globals.maxIceCount * Grade.perfectAddInPercentages[order.iceAmount]);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.A));
        }

        [Test]
        public void CalculateAGradeHalfFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Half,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew, Order.Flavor.Coconut },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount * Grade.perfectAddInPercentages[order.bobaAmount]);
            Globals.iceCount = (int)(Globals.maxIceCount * Grade.perfectAddInPercentages[order.iceAmount]);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 0.5f },
                { Order.Flavor.Coconut, 0.5f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.A));
        }

        [Test]
        public void CalculateAGradeSplashFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Splash,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew, Order.Flavor.Coconut },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount * Grade.perfectAddInPercentages[order.bobaAmount]);
            Globals.iceCount = (int)(Globals.maxIceCount * Grade.perfectAddInPercentages[order.iceAmount]);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 0.75f },
                { Order.Flavor.Coconut, 0.25f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.A));
        }

        [Test]
        public void CalculateCGradeWithAddIns() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount / 2);
            Globals.iceCount = (int)(Globals.maxIceCount / 2);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.C));
        }

        [Test]
        public void CalculateCGradeWithFlavors() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount * Grade.perfectAddInPercentages[order.bobaAmount]);
            Globals.iceCount = (int)(Globals.maxIceCount * Grade.perfectAddInPercentages[order.iceAmount]);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 0.9f },
                { Order.Flavor.Strawberry, 0.1f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.C));
        }

        [Test]
        public void CalculateFGradeWithAddIns() {
            Order order = new Order {
                iceAmount = Order.AddInOption.None,
                bobaAmount = Order.AddInOption.None,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount);
            Globals.iceCount = (int)(Globals.maxIceCount);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.F));
        }

        [Test]
        public void CalculateFGradeWithFlavors() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount);
            Globals.iceCount = (int)(Globals.maxIceCount);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 0.5f },
                { Order.Flavor.Strawberry, 0.5f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.F));
        }

        [Test]
        public void CompileDrinkNameWithNoIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.None,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew bubble tea with no ice"));
        }

        [Test]
        public void CompileDrinkNameWithLightIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Light,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew bubble tea with light ice"));
        }

        [Test]
        public void CompileDrinkNameWithRegularIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithExtraIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Extra,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew bubble tea with extra ice"));
        }

        [Test]
        public void CompileToSentenceStringWithNoBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.None,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew tea"));
        }

        [Test]
        public void CompileToSentenceStringWithLightBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Light,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew bubble tea with light boba"));
        }

        [Test]
        public void CompileToSentenceStringWithRegularBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithExtraBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Extra,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew bubble tea with extra boba"));
        }

        [Test]
        public void CompileToSentenceStringWithSingleFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew, Order.Flavor.Coconut },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 1.0f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew bubble tea"));
        }


        [Test]
        public void CompileToSentenceStringWithHalfFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Half,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew, Order.Flavor.Coconut },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 0.5f },
                { Order.Flavor.Coconut, 0.5f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew coconut bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithSplashFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Splash,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew, Order.Flavor.Coconut },
            };
            Globals.currentOrder = order;
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float> {
                { Order.Flavor.Honeydew, 0.75f },
                { Order.Flavor.Coconut, 0.25f },
            };

            Grade grade = Grade.Compile();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew with a splash of coconut bubble tea"));
        }
    }
}
