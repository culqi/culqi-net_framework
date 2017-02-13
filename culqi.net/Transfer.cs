using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Transfer
	{	
		const string URL = "/transfers/";

		Security security { get; set; }

		public Transfer(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, string> query_params)
		{
			return new Util().Request(query_params, URL, security.api_key, "get");
		}

		public string Get(String id)
		{
			return new Util().Request(null, URL + id + "/", security.api_key, "get");
		}

	}
}
