using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests {
    public class GradeTest {
        [Test]
        public void CalculateAGrade() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount * Grade.perfectAddInPercentages[order.bobaAmount]);
            Globals.iceCount = (int)(Globals.maxIceCount * Grade.perfectAddInPercentages[order.iceAmount]);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float>();

            Grade grade = new Grade();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.A));
        }

        [Test]
        public void CalculateCGradeWithAddIns() {
            Order order = new Order {
                iceAmount = Order.AddInOption.None,
                bobaAmount = Order.AddInOption.None,
                drinkType = Order.DrinkType.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Honeydew },
            };

            Globals.currentOrder = order;
            Globals.bobaCount = (int)(Globals.maxBobaCount);
            Globals.iceCount = (int)(Globals.maxIceCount);
            Globals.liquidPercentages = new Dictionary<Order.Flavor, float>();

            Grade grade = new Grade();

            Assert.That(grade.letterGrade, Is.EqualTo(Grade.LetterGrade.C));
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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

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
            Grade grade = new Grade();

            Assert.That(grade.drinkName, Is.EqualTo("Honeydew with a splash of coconut bubble tea"));
        }
    }
}
