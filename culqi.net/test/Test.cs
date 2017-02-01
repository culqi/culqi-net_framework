using System;
using System.IO;
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
			path = path.Replace(".", ""); // Remove period.
			return path;
		}


		protected string CreateToken()
		{
			TokenModel tokenModel = new TokenModel();
			tokenModel.card_number = "4111111111111111";
			tokenModel.currency_code = "PEN";
			tokenModel.cvv = "123";
			tokenModel.expiration_month = 9;
			tokenModel.expiration_year = 2020;
			tokenModel.fingerprint = "q352454534";
			tokenModel.last_name = "Muro";
			tokenModel.email = "wmuro@me.com";
			tokenModel.first_name = "William";
			return new Token(security).Create(tokenModel);
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

			ChargeModel chargeModel = new ChargeModel();
			chargeModel.address = "Avenida Lima 1232";
			chargeModel.address_city = "LIMA";
			chargeModel.amount = 1000;
			chargeModel.country_code = "PE";
			chargeModel.currency_code = "PEN";
			chargeModel.email = "wmuro@me.com";
			chargeModel.first_name = "William";
			chargeModel.installments = 0;
			chargeModel.last_name = "Muro";
			chargeModel.metadata = "";
			chargeModel.phone_number = 3333339;
			chargeModel.product_description = "Venta de prueba";
			chargeModel.token_id = (string)json_object["id"];
			return new Charge(security).Create(chargeModel);

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
			PlanModel planModel = new PlanModel();
			planModel.alias = "plan-culqi-"+GetRandomString();
			planModel.amount = 1000;
			planModel.currency_code = "PEN";
			planModel.interval = "month";
			planModel.interval_count = 1;
			planModel.limit = 12;
			planModel.name = "Plan de Prueba "+GetRandomString();
			planModel.trial_days = 15;
			return new Plan(security).Create(planModel);
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

			SubscriptionModel subscriptionModel = new SubscriptionModel();
			subscriptionModel.address = "Avenida Lima 123213";
			subscriptionModel.address_city = "LIMA";
			subscriptionModel.country_code = "PE";
			subscriptionModel.email = "wmuro@me.com";
			subscriptionModel.last_name = "Muro";
			subscriptionModel.first_name = "William";
			subscriptionModel.phone_number = 1234567789;
			subscriptionModel.plan_alias = (string)json_plan["alias"];
			subscriptionModel.token_id = (string)json_token["id"];
			return new Subscription(security).Create(subscriptionModel);
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

			RefundModel refundModel = new RefundModel();
			refundModel.amount = 500;
			refundModel.charge_id = (string)json_object["id"];
			refundModel.reason = "bought an incorrect product";

			return new Refund(security).Create(refundModel);
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
