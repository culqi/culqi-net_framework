using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace culqi.net
{
	public class Charge : Generic
	{	
		const string URL = "/charges/";

		public Charge(Security security) : base(security, URL)
        {
	
		}

	}
}
