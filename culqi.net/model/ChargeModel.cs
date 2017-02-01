using System;
namespace culqi.net
{
	public class ChargeModel
	{
		public ChargeModel()
		{
		}

		public const string URL = "/charges/";

		public string address { get; set; }

		public string address_city { get; set; }

		public int amount { get; set; }

		public string country_code { get; set; }

		public string currency_code { get; set; }

		public string email { get; set; }

		public string first_name { get; set; }

		public int installments { get; set; }

		public string last_name { get; set; }

		public string metadata { get; set; }

		public int phone_number { get; set; }

		public string product_description { get; set; }

		public string token_id { get; set; }

	}
}
