using System;
namespace culqi.net
{
	public class TokenModel
	{	
		public TokenModel()
		{
		}

		public const string URL = "/tokens/";

		public string card_number{ get; set; }

		public string currency_code{ get; set; }

		public string cvv{ get; set; }

		public int expiration_month{ get; set; }

		public int expiration_year{ get; set; }

		public string fingerprint{ get; set; }

		public string last_name{ get; set; }

		public string email{ get; set; }

		public string first_name{ get; set; }

	}
}
