﻿using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace culqi.net
{	
	[TestFixture]
	public class TestCreate
	{
		Security security = null;

		public TestCreate()
		{
			security = new Security();
			security.code_commerce = "pk_test_vzMuTHoueOMlgUPj";
			security.api_key = "sk_test_UTCQSGcXW8bCyU59";
		}

		protected static string GetRandomString()
		{
			string path = Path.GetRandomFileName();
			path = path.Replace(".", "");
			return path;
		}

		protected string CreateToken()
		{	
			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"card_number", "4111111111111111"},
				{"cvv", "123"},
				{"expiration_month", 9},
				{"expiration_year", 2020},
				{"email", "wmuro@me.com"}
			};
			return new Token(security).Create(map);
		}

		[Test]
		public void ValidCreateToken()
		{
			string data = CreateToken();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("token",(string)json_object["object"]);
		}

		protected string CreateCharge()
		{	

			string data = CreateToken();

			var json_object = JObject.Parse(data);

			Dictionary<string, object> metadata = new Dictionary<string, object>
			{
				{"order_id", "777"}
			};

			Dictionary<string, object> map = new Dictionary<string, object>
			{	
				{"amount", 1000},
				{"capture", true},
				{"currency_code", "PEN"},
				{"description", "Venta de prueba"},
				{"email", "wmuro@me.com"},
				{"installments", 0},
				{"metadata", metadata},
				{"source_id", (string)json_object["id"]}
			};

			return new Charge(security).Create(map);

		}

		[Test]
		public void ValidCreateCharge()
		{
			string data = CreateCharge();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("charge", (string)json_object["object"]);
		}

		protected string CreatePlan()
		{	

			Dictionary<string, object> metadata = new Dictionary<string, object>
			{
				{"others_id", "9092"}
			};

			Dictionary<string, object> map = new Dictionary<string, object>
			{	
				{"amount", 10000},
				{"currency_code", "PEN"},
				{"interval", "dias"},
				{"interval_count", 15},
				{"limit", 2},
				{"metadata", metadata},
				{"name", "plan-culqi-"+GetRandomString()},
				{"trial_days", 15}
			};

			return new Plan(security).Create(map);
		}

		[Test]
		public void ValidCreatePlan()
		{
			string data = CreatePlan();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("plan", (string)json_object["object"]);
		}

		protected string CreateCustomer()
		{
			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"address", "Av Lima 123"},
				{"address_city", "Lima"},
				{"country_code", "PE"},
				{"email", "test"+GetRandomString()+"@culqi.com"},
				{"first_name", "Test"},
				{"last_name", "Culqi"},
				{"phone_number", 99004356}
			};

			return new Customer(security).Create(map);
		}

		[Test]
		public void ValidCreateCustomer()
		{
			string data = CreateCustomer();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("customer", (string)json_object["object"]);
		}

		protected string CreateCard()
		{

			string token = CreateToken();
			string customer = CreateCustomer();

			var json_token = JObject.Parse(token);
			var json_customer = JObject.Parse(customer);

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"customer_id", (string)json_customer["id"]},
				{"token_id", (string)json_token["id"]}
			};

			return new Card(security).Create(map);

		}

		[Test]
		public void ValidCreateCard()
		{
			string data = CreateCard();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("card", (string)json_object["object"]);
		}


		protected string CreateSubscription()
		{	
			string plan_data = CreatePlan();
			var json_plan = JObject.Parse(plan_data);

			string card_data = CreateCard();
			var json_card = JObject.Parse(card_data);

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"card_id", (string)json_card["id"]},
				{"plan_id", (string)json_plan["id"]}
			};

			return new Subscription(security).Create(map);
		}

		[Test]
		public void ValidCreateSubscription()
		{
			string data = CreateSubscription();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("subscription", (string)json_object["object"]);
		}

		[Test]
		public void GetChargeCapture()
		{	
			string charge_data = CreateCharge();

			var json_charge = JObject.Parse(charge_data);

			string capture_data = new Charge(security).Capture((string)json_charge["id"]);

			var json_capture = JObject.Parse(capture_data);

			Assert.AreNotSame("charge", (string)json_capture["id"]);
		}

		/*protected string CreateRefund()
		{	
			string data = CreateCharge();

			var json_object = JObject.Parse(data);

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"amount", 500},
				{"charge_id", (string)json_object["id"]},
				{"reason", "bought an incorrect product"}
			};

			return new Refund(security).Create(map);
		}

		[Test]
		public void ValidCreateRefund()
		{
			string data = CreateRefund();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("refund", (string)json_object["object"]);
		}*/

	}
}
