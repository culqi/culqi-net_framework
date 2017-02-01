using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Subscription
	{
		Security security { get; set; }

		public Subscription(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, string> query_params)
		{
			Util util = new Util();
			return util.Request(query_params, SubscriptionModel.URL, security.api_key, "get");
		}

		public string Create(SubscriptionModel subscription)
		{
			Util util = new Util();
			return util.Request(subscription, SubscriptionModel.URL, security.api_key, "post");
		}

		public string Get(String id)
		{
			Util util = new Util();
			return util.Request(null, SubscriptionModel.URL + id + "/", security.api_key, "get");
		}

		public string Delete(String id)
		{
			Util util = new Util();
			return util.Request(null, SubscriptionModel.URL + id + "/", security.api_key, "delete");
		}

	}
}
