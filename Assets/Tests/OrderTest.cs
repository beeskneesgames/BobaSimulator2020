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
    }
}
