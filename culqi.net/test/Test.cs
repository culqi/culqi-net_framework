using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace culqi.net
{	
	[TestFixture]
	public class Test
	{
		Security security = null;

		public Test()
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
				{"currency_code", "PEN"},
				{"cvv", "123"},
				{"expiration_month", 9},
				{"expiration_year", 2020},
				{"fingerprint", "q352454534"},
				{"last_name", "Muro"},
				{"email", "wmuro@me.com"},
				{"first_name", "William"}
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

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"address", "Avenida Lima 1232"},
				{"address_city", "LIMA"},
				{"amount", 1000},
				{"country_code", "PE"},
				{"currency_code", "PEN"},
				{"email", "wmuro@me.com"},
				{"first_name", "William"},
				{"installments", 0},
				{"last_name", "Muro"},
				{"metadata", ""},
				{"phone_number", 3333339},
				{"product_description", "Venta de prueba"},
				{"token_id", (string)json_object["id"]}
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

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"alias", "plan-culqi-"+GetRandomString()},
				{"amount", 1000},
				{"currency_code", "PEN"},
				{"interval", "month"},
				{"interval_count", 1},
				{"limit", 12},
				{"name", "Plan de Prueba "+GetRandomString()},
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

		protected string CreateSubscription()
		{	
			string plan_data = CreatePlan();
			var json_plan = JObject.Parse(plan_data);

			string token_data = CreateToken();
			var json_token = JObject.Parse(token_data);

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"address", "Avenida Lima 123213"},
				{"address_city", "LIMA"},
				{"country_code", "PE"},
				{"email", "wmuro@me.com"},
				{"last_name", "Muro"},
				{"first_name", "William"},
				{"phone_number", 1234567789},
				{"plan_alias", (string)json_plan["alias"]},
				{"token_id", (string)json_token["id"]}
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

		protected string CreateRefund()
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
		}

	}
}
