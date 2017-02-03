using System;
namespace culqi.net
{
	public class Iin
	{	
		const string URL = "/iins/";

		Security security { get; set; }

		public Iin(Security security)
		{
			this.security = security;
		}

		public string Get(String id)
		{
			return new Util().Request(null, URL + id + "/", security.api_key, "get");
		}

	}
}
