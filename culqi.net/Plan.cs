using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Plan
	{	
		Security security { get; set; }

		public Plan(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, string> query_params)
		{
			Util util = new Util();
			return util.Request(query_params, PlanModel.URL, security.api_key, "get");
		}

		public string Create(PlanModel plan)
		{
			Util util = new Util();
			return util.Request(plan, PlanModel.URL, security.api_key, "post");
		}

		public string Get(String id)
		{
			Util util = new Util();
			return util.Request(null, PlanModel.URL + id + "/", security.api_key, "get");
		}

	}
}
