using System;
namespace culqi.net
{
	public class Token
	{	
		Security security { get; set; }

		public Token(Security security)
		{
			this.security = security;
		}

		public string Create(TokenModel token)
		{
			Util util = new Util();
			return util.Request(token, TokenModel.URL, security.code_commerce, "post");
		}

		public string Get(String id)
		{
			Util util = new Util();
			return util.Request(null, TokenModel.URL + id + "/", security.api_key, "get");
		}

	}
}
