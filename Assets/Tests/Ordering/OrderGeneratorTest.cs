using System.Collections.Generic;
using NUnit.Framework;

namespace Tests {
    public class OrderGeneratorTest {
        [Test]
        public void OrderGeneratorGeneratesBasicOrder() {
            Order order = OrderGenerator.GenerateBasicOrder();
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order expectedOrder = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = order.drinkFlavors,
            };

            //Assert.That(order, Is.SameAs(expectedOrder));
            Assert.That(order.drinkFlavors, Is.InstanceOf(typeof(List<Order.Flavor>)));
        }
    }
}
