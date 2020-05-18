using System.Collections.Generic;
using NUnit.Framework;

namespace Tests {
    public class OrderTest {
        [Test]
        public void OrderIsBubbleTea() {
            Order order = new Order { bobaAmount = Order.AddInOption.Regular };

            Assert.AreEqual(expected: true, actual: order.IsBubbleTea());
        }

        [Test]
        public void OrderNotBubbleTea() {
            Order order = new Order();

            Assert.AreEqual(expected: false, actual: order.IsBubbleTea());
        }

        [Test]
        public void OrderIsValid() {
            Order order = new Order {
                iceAmount = Order.AddInOption.None,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.IsValid(), Is.True);
        }

        [Test]
        public void OrderIsNotValid() {
            Order order = new Order();

            Assert.That(order.IsValid(), Is.False);
        }


        [Test]
        public void OrderIsNotValidWithDuplicateFlavors() {
            Order order = new Order {
                iceAmount = Order.AddInOption.None,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> {
                    Order.Flavor.Mango,
                    Order.Flavor.Mango,
                },
            };

            Assert.That(order.IsValid(), Is.False);
        }

        [Test]
        public void CompileToSentenceStringWithNoIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.None,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with no ice"));
        }

        [Test]
        public void CompileToSentenceStringWithLightIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Light,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with light ice"));
        }

        [Test]
        public void CompileToSentenceStringWithRegularIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithExtraIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Extra,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with extra ice"));
        }

        [Test]
        public void CompileToSentenceStringWithNoBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.None,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango tea"));
        }

        [Test]
        public void CompileToSentenceStringWithLightBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Light,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with light boba"));
        }

        [Test]
        public void CompileToSentenceStringWithRegularBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithExtraBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Extra,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with extra boba"));
        }

        [Test]
        public void CompileToSentenceStringWithSingleFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango, Order.Flavor.Blueberry },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea"));
        }


        [Test]
        public void CompileToSentenceStringWithHalfFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Half,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango, Order.Flavor.Blueberry },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango blueberry bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithSplashFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Splash,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango, Order.Flavor.Blueberry },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango with a splash of blueberry bubble tea"));
        }
    }
}
