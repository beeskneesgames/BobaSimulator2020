using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests {
    public class OrderGeneratorTest {
        [Test]
        public void GeneratesBasicOrder() {
            List<Order.Flavor> flavor = new List<Order.Flavor> { Order.Flavor.Mango };
            Order order = OrderGenerator.GenerateBasicOrder(flavor);

            Assert.That(order.IsValid(), Is.True);
            Assert.That(order.iceAmount, Is.EqualTo(Order.AddInOption.Regular));
            Assert.That(order.bobaAmount, Is.EqualTo(Order.AddInOption.Regular));
            Assert.That(order.drinkType, Is.EqualTo(Order.FlavorOption.Single));
            Assert.That(order.drinkFlavors.First(), Is.EqualTo(Order.Flavor.Mango));
            Assert.That(order.drinkFlavors.Count, Is.EqualTo(1));
        }

        [Test]
        public void GeneratesRandomOrder() {
            Order order = OrderGenerator.GenerateRandomOrder();

            Assert.That(order.IsValid(), Is.True);
        }
    }
}
