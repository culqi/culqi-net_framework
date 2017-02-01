using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Charge
	{
		Security security { get; set; }

		public Charge(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, string> query_params)
		{
			Util util = new Util();
			return util.Request(query_params, ChargeModel.URL, security.api_key, "get");
		}

		public string Create(ChargeModel charge)
		{	
			Util util = new Util();
			return util.Request(charge, ChargeModel.URL, security.api_key, "post");
		}

		public string Get(String id)
		{
			Util util = new Util();
			return util.Request(null, ChargeModel.URL+id+"/", security.api_key, "get");
		}

	}
}
