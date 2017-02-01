using System;
namespace culqi.net
{
	public class Iin
	{	
		Security security { get; set; }

		public Iin(Security security)
		{
			this.security = security;
		}

		public string Get(String id)
		{
			Util util = new Util();
			return util.Request(null, IinsModel.URL + id + "/", security.api_key, "get");
		}

	}
}
