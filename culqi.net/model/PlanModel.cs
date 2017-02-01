using System;
namespace culqi.net
{
	public class PlanModel
	{
		public PlanModel()
		{
		}

		public const string URL = "/plans/";

		public string alias { get; set; }

		public int amount { get; set; }

		public string currency_code { get; set; }

		public string interval { get; set; }

		public int interval_count { get; set; }

		public int limit { get; set; }

		public string name { get; set; }

		public int trial_days { get; set; }

	}
}
