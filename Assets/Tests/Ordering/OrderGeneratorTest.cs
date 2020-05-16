using System.Collections.Generic;
using NUnit.Framework;

namespace Tests {
    public class OrderGeneratorTest {
        [Test]
        public void GeneratesBasicOrder() {
            Order order = OrderGenerator.GenerateBasicOrder();
            Order expectedOrder = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = order.drinkFlavors,
            };

            Assert.That(order.iceAmount, Is.EqualTo(expectedOrder.iceAmount));
            Assert.That(order.bobaAmount, Is.EqualTo(expectedOrder.bobaAmount));
            Assert.That(order.drinkType, Is.EqualTo(expectedOrder.drinkType));
            Assert.That(order.drinkFlavors, Is.InstanceOf(typeof(List<Order.Flavor>)));
        }

        [Test]
        public void GeneratesRandomOrder() {
            Order order = OrderGenerator.GenerateRandomOrder();

            Assert.That(order.iceAmount, Is.InstanceOf(typeof(Order.AddInOption)));
            Assert.That(order.bobaAmount, Is.InstanceOf(typeof(Order.AddInOption)));
            Assert.That(order.drinkType, Is.InstanceOf(typeof(Order.FlavorOption)));
            Assert.That(order.drinkFlavors, Is.InstanceOf(typeof(List<Order.Flavor>)));
        }
    }
}
