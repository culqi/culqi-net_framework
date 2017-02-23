using System;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace culqi.net
{	
	[TestFixture]
	public class TestPatch
	{	
		Security security = null;

		public TestPatch()
		{
			security = new Security();
			security.secret_key = "sk_test_UTCQSGcXW8bCyU59";
		}

		protected string UpdatePlan()
		{
			Dictionary<string, object> metadata = new Dictionary<string, object>
			{
				{"abc", "555"}
			};

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"metadata", metadata}
			};

			return new Plan(security).Update(map,"pln_test_pLFzcWkwj33xFGF1");
		}

		[Test]
		public void ValidCreatePlan()
		{
			string data = UpdatePlan();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("plan", (string)json_object["object"]);
		}

	}
}
