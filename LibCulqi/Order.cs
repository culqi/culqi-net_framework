using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace culqi.net
{
	public class Order : Generic
    {	
		const string URL = "/orders/";

		public Order(Security security) : base(security, URL)
        {
			
		}

	}
}
