using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Token
	{
		const string URL = "/tokens/";

		Security security { get; set; }

		public Token(Security security)
		{
			this.security = security;
		}

		public string Create(Dictionary<string, object> body)
		{
			return new Util().Request(body, URL, security.code_commerce, "post");
		}

		public string Get(String id)
		{
			return new Util().Request(null, URL + id + "/", security.api_key, "get");
		}

	}
}
