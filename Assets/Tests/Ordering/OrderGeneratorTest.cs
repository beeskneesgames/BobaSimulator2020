using System.Collections.Generic;
using NUnit.Framework;

namespace Tests {
    public class OrderGeneratorTest {
        [Test]
        public void GeneratesBasicOrder() {
            List<Order.Flavor> flavor = new List<Order.Flavor> { Order.Flavor.Mango };
            Order order = OrderGenerator.GenerateBasicOrder(flavor);

            Assert.That(order.iceAmount, Is.EqualTo(Order.AddInOption.Regular));
            Assert.That(order.bobaAmount, Is.EqualTo(Order.AddInOption.Regular));
            Assert.That(order.drinkType, Is.EqualTo(Order.FlavorOption.Single));
            Assert.That(order.drinkFlavors, Is.EqualTo(Order.Flavor.Mango));
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
