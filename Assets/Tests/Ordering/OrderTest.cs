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
        public void CompileToSentenceStringWithNoIce() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.None,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with no ice"));
        }

        [Test]
        public void CompileToSentenceStringWithLightIce() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Light,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with light ice"));
        }

        [Test]
        public void CompileToSentenceStringWithRegularIce() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithExtraIce() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Extra,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with extra ice"));
        }

        [Test]
        public void CompileToSentenceStringWithNoBoba() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.None,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango tea"));
        }

        [Test]
        public void CompileToSentenceStringWithLightBoba() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Light,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with light boba"));
        }

        [Test]
        public void CompileToSentenceStringWithRegularBoba() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithExtraBoba() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Extra,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea with extra boba"));
        }

        [Test]
        public void CompileToSentenceStringWithSingleFlavor() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango, Order.Flavor.Blueberry },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango bubble tea"));
        }


        [Test]
        public void CompileToSentenceStringWithHalfFlavor() {
            Order.FlavorOption drinkType = Order.FlavorOption.Half;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango, Order.Flavor.Blueberry },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango blueberry bubble tea"));
        }

        [Test]
        public void CompileToSentenceStringWithSplashFlavor() {
            Order.FlavorOption drinkType = Order.FlavorOption.Splash;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango, Order.Flavor.Blueberry },
            };

            Assert.That(order.ToSentence(), Is.EqualTo("Mango with a splash of blueberry bubble tea"));
        }
    }
}
