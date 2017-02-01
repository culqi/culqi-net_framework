using System;
namespace culqi.net
{
	public class SubscriptionModel
	{
		public SubscriptionModel()
		{
		}

		public const string URL = "/subscriptions/";

		public string address { get; set; }

		public string address_city { get; set; }

		public string country_code { get; set; }

		public string email { get; set; }

		public string last_name { get; set; }

		public string first_name { get; set; }

		public int phone_number { get; set; }

		public string plan_alias { get; set; }

		public string token_id { get; set; }

	}
}
