using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Subscription
	{	

		const string URL = "/subscriptions/";

		Security security { get; set; }

		public Subscription(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, string> query_params)
		{
			return new Util().Request(query_params, URL, security.api_key, "get");
		}

		public string Create(Dictionary<string, object> body)
		{
			return new Util().Request(body, URL, security.api_key, "post");
		}

		public string Get(String id)
		{
			return new Util().Request(null, URL + id + "/", security.api_key, "get");
		}

		public string Delete(String id)
		{
			return new Util().Request(null, URL + id + "/", security.api_key, "delete");
		}

	}
}
