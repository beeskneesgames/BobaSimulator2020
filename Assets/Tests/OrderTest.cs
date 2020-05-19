using System.Collections.Generic;
using System.Linq;
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
        public void GeneratesBasicOrder() {
            List<Order.Flavor> flavor = new List<Order.Flavor> { Order.Flavor.Mango };
            Order order = Order.GenerateBasic(flavor);

            Assert.That(order.IsValid(), Is.True);
            Assert.That(order.iceAmount, Is.EqualTo(Order.AddInOption.Regular));
            Assert.That(order.bobaAmount, Is.EqualTo(Order.AddInOption.Regular));
            Assert.That(order.drinkType, Is.EqualTo(Order.FlavorOption.Single));
            Assert.That(order.drinkFlavors.First(), Is.EqualTo(Order.Flavor.Mango));
            Assert.That(order.drinkFlavors.Count, Is.EqualTo(1));
        }

        [Test]
        public void GeneratesRandomOrder() {
            Order order = Order.GenerateRandom();

            Assert.That(order.IsValid(), Is.True);
        }

        [Test]
        public void IceDescriptionNoIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.None,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.IceDescription, Is.EqualTo("No ice"));
        }

        [Test]
        public void IceDescriptionLightIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Light,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.IceDescription, Is.EqualTo("Light ice"));
        }

        [Test]
        public void IceDescriptionRegularIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.IceDescription, Is.EqualTo("Ice"));
        }

        [Test]
        public void IceDescriptionExtraIce() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Extra,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.IceDescription, Is.EqualTo("Extra ice"));
        }

        [Test]
        public void BobaDescriptionNoBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.None,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.BobaDescription, Is.EqualTo("No boba"));
        }

        [Test]
        public void BobaDescriptionLightBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Light,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.BobaDescription, Is.EqualTo("Light boba"));
        }

        [Test]
        public void BobaDescriptionRegularBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.BobaDescription, Is.EqualTo("Boba"));
        }

        [Test]
        public void BobaDescriptionExtraBoba() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Extra,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.BobaDescription, Is.EqualTo("Extra boba"));
        }

        [Test]
        public void FlavorDescriptionSingleFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango },
            };

            Assert.That(order.FlavorDescription, Is.EqualTo("Mango"));
        }

        [Test]
        public void FlavorDescriptionHalfFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Half,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango, Order.Flavor.Blueberry },
            };

            Assert.That(order.FlavorDescription, Is.EqualTo("Mango blueberry"));
        }

        [Test]
        public void FlavorDescriptionSplashFlavor() {
            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Splash,
                drinkFlavors = new List<Order.Flavor> { Order.Flavor.Mango, Order.Flavor.Blueberry },
            };

            Assert.That(order.FlavorDescription, Is.EqualTo("Mango with a splash of blueberry"));
        }
    }
}
