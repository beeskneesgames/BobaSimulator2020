using System.Collections.Generic;
using NUnit.Framework;

namespace Tests {
    public class OrderGeneratorTest {
        [Test]
        public void OrderGeneratorGeneratesBasicOrder() {
            Order order = OrderGenerator.GenerateBasicOrder();
            Order expectedOrder = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = Order.FlavorOption.Single,
                drinkFlavors = order.drinkFlavors,
            };

            Assert.That(order.iceAmount, Is.EqualTo(expectedOrder.iceAmount));
            Assert.That(order.bobaAmount, Is.EqualTo(expectedOrder.bobaAmount));
            Assert.That(order.iceAmount, Is.EqualTo(expectedOrder.drinkType));
            Assert.That(order.drinkFlavors, Is.InstanceOf(typeof(List<Order.Flavor>)));
        }
    }
}
