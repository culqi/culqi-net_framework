using System;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace culqi.net
{
    [TestFixture]
    public class TestPatch
    {
        CulqiCRUD culqiCRUD = new CulqiCRUD();
        Security security = null;


        [Test]
        public void Test01_UpdatePlan()
        {
            HttpResponseMessage data = culqiCRUD.UpdatePlan();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(json_object.ContainsKey("id"));
            Assert.IsTrue(json_object.ContainsKey("interval_unit_time"));
            Assert.IsTrue(json_object.ContainsKey("interval_count"));
            Assert.IsTrue(json_object.ContainsKey("amount"));
            Assert.IsTrue(json_object.ContainsKey("name"));
            Assert.IsTrue(json_object.ContainsKey("description"));
            Assert.IsTrue(json_object.ContainsKey("short_name"));
            Assert.IsTrue(json_object.ContainsKey("currency"));
            Assert.IsTrue(json_object.ContainsKey("initial_cycles"));
            Assert.IsTrue(json_object.ContainsKey("metadata"));
            Assert.IsTrue(json_object.ContainsKey("total_subscriptions"));
            Assert.IsTrue(json_object.ContainsKey("status"));
            Assert.IsTrue(json_object.ContainsKey("creation_date"));
            Assert.IsTrue(json_object.ContainsKey("slug"));
        }


        [Test]
        public void Test02_UpdateOrder()
        {
            HttpResponseMessage data = culqiCRUD.UpdateOrder();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("order", (string)json_object["object"]);
        }


        [Test]
        public void Test03_UpdateCharge()
        {
            HttpResponseMessage data = culqiCRUD.UpdateCharge();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("charge", (string)json_object["object"]);
        }


        [Test]
        public void Test04_UpdateCard()
        {
            HttpResponseMessage data = culqiCRUD.UpdateCard();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("card", (string)json_object["object"]);
        }

        [Test]
        public void Test05_UpdateSubscription()
        {
            HttpResponseMessage data = culqiCRUD.UpdateSubscription();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("subscription", (string)json_object["object"]);
        }

    }
}