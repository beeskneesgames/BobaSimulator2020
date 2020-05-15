using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class Order {
        [Test]
        public void OrderIsBubbleTea() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.Regular,
                drinkType = drinkType,
                drinkFlavors = Order.Flavor.Mango,
            };

            Assert.AreEqual(expected: true, actual: order.IsBubbleTea());
        }

        public void OrderNotBubbleTea() {
            Order.FlavorOption drinkType = Order.FlavorOption.Single;

            Order order = new Order {
                iceAmount = Order.AddInOption.Regular,
                bobaAmount = Order.AddInOption.None,
                drinkType = drinkType,
                drinkFlavors = Order.Flavor.Mango,
            };

            Assert.AreEqual(expected: false, actual: order.IsBubbleTea());
        }
    }
