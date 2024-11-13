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

            Assert.That(json_object.ContainsKey("id"), Is.True);
            Assert.That(json_object.ContainsKey("interval_unit_time"), Is.True);
            Assert.That(json_object.ContainsKey("interval_count"), Is.True);
            Assert.That(json_object.ContainsKey("amount"), Is.True);
            Assert.That(json_object.ContainsKey("name"), Is.True);
            Assert.That(json_object.ContainsKey("description"), Is.True);
            Assert.That(json_object.ContainsKey("short_name"), Is.True);
            Assert.That(json_object.ContainsKey("currency"), Is.True);
            Assert.That(json_object.ContainsKey("initial_cycles"), Is.True);
            Assert.That(json_object.ContainsKey("metadata"), Is.True);
            Assert.That(json_object.ContainsKey("total_subscriptions"), Is.True);
            Assert.That(json_object.ContainsKey("status"), Is.True);
            Assert.That(json_object.ContainsKey("creation_date"), Is.True);
            Assert.That(json_object.ContainsKey("slug"), Is.True);
        }


        [Test]
        public void Test02_UpdateOrder()
        {
            HttpResponseMessage data = culqiCRUD.UpdateOrder();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("order", (string)json_object["object"]);
        }


        [Test]
        public void Test03_UpdateCharge()
        {
            HttpResponseMessage data = culqiCRUD.UpdateCharge();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("charge", (string)json_object["object"]);
        }


        [Test]
        public void Test04_UpdateCard()
        {
            HttpResponseMessage data = culqiCRUD.UpdateCard();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("card", (string)json_object["object"]);
        }

        [Test]
        public void Test05_UpdateSubscription()
        {
            HttpResponseMessage data = culqiCRUD.UpdateSubscription();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Console.WriteLine(json_object);
            //Assert.AreEqual("plan", (string)json_object["object"]);
            Assert.That(json_object.ContainsKey("id"), Is.True);

        }

    }
}