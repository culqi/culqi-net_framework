using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace culqi.net
{	
	[TestFixture]
	public class TestCreate
	{
		CulqiCRUD culqiCRUD = new CulqiCRUD();


		[Test]
		public void Test01_CreateToken()
		{
			//var data = culqiCRUD.CreateToken().Content;
            HttpResponseMessage data = culqiCRUD.CreateToken();
            string jsonContent = data.Content.ReadAsStringAsync().Result;
            var json_object = JObject.Parse(jsonContent);
            TestContext.WriteLine(json_object.ToString());
            Assert.That((string)json_object["object"], Is.EqualTo("token"));
        }
        
        [Test]
        public void Test02_CreateTokenEncrypt()
        {
            HttpResponseMessage data = culqiCRUD.CreateTokenEncrypt();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            TestContext.WriteLine(json_object.ToString());
            Assert.That((string)json_object["object"], Is.EqualTo("token"));
        }
        
        [Test]
        public void Test03_CreateTokenYape()
        {
            HttpResponseMessage data = culqiCRUD.CreateTokenYape();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Assert.Equals("token", (string)json_object["object"]);
        }

		[Test]
		public void Test04_CreateCharge()
		{
            HttpResponseMessage data = culqiCRUD.CreateCharge();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Assert.Equals("charge", (string)json_object["object"]);
		}

        [Test]
        public void Test05_CreateChargeEncrypt()
        {
            HttpResponseMessage data = culqiCRUD.CreateChargeEncrypt();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("charge", (string)json_object["object"]);
        }

        [Test]
        public void Test06_ChargeCapture()
        {
            HttpResponseMessage capture_data = culqiCRUD.CreateChargeCapture();

            var json_capture = JObject.Parse(capture_data.Content.ReadAsStringAsync().Result);

            Assert.That((string)json_capture["id"], Is.Not.SameAs("charge"));
        }

        [Test]
        public void Test07_CreateOrder()
        {
            HttpResponseMessage data = culqiCRUD.CreateOrder();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("order", (string)json_object["object"]);
        }

        [Test]
        public void Test08_CreateOrderEncrypt()
        {
            HttpResponseMessage data = culqiCRUD.CreateOrderEncrypt();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("order", (string)json_object["object"]);
        }

        [Test]
        public void Test09_ConfirmOrder()
        {
            HttpResponseMessage data = culqiCRUD.CreateOrder();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("order", (string)json_object["object"]);
        }

        [Test]
		public void Test10_CreatePlan()
		{
            HttpResponseMessage data = culqiCRUD.CreatePlan();

			var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.That(json_object.ContainsKey("id"), Is.True);
            Assert.That(json_object.ContainsKey("slug"), Is.True);
		}
     
		[Test]
		public void Test11_CreateCustomer()
		{
            HttpResponseMessage data = culqiCRUD.CreateCustomer();

			var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("customer", (string)json_object["object"]);
		}

		[Test]
		public void Test12_CreateCard()
		{
            HttpResponseMessage data = culqiCRUD.CreateCard();

			var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            ;
            Assert.Equals("card", (string)json_object["object"]);
		}

		[Test]
		public void Test13_CreateSubscription()
		{
            HttpResponseMessage data = culqiCRUD.CreateSubscription();

			var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.That(json_object.ContainsKey("id"), Is.True);
            Assert.That(json_object.ContainsKey("plan_id"), Is.True);	
        }

        [Test]
        public void Test14_CreateRefund()
        {
            HttpResponseMessage data = culqiCRUD.CreateRefund();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.Equals("refund", (string)json_object["object"]);
        }

        [Test]
        public void Test15_CreateChargeWithCustomHeader()
        {
            try
            {
                HttpResponseMessage data = culqiCRUD.CreateChargeWithCustomHeader();
                if (data.IsSuccessStatusCode)
                {
                    var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
                    Assert.That(json_object.ContainsKey("id"), Is.True);
                }
                else
                {
                    Assert.Fail("La solicitud no fue exitosa.");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Error durante la ejecución de la prueba.");
            }
        }

        [Test]
        public void Test16_CreateChargeEncryptWithCustomHeader()
        {
            try
            {
                HttpResponseMessage data = culqiCRUD.CreateChargeEncryptWithCustomHeader();
                if (data.IsSuccessStatusCode)
                {
                    var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
                    Assert.That(json_object.ContainsKey("id"), Is.True);
                }
                else
                {
                    Assert.Fail("La solicitud no fue exitosa.");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Error durante la ejecución de la prueba.");
            }
        }
    }

}
