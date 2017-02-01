using System;
namespace culqi.net
{
	public class RefundModel
	{
		public RefundModel()
		{
		}

		public const string URL = "/refunds/";

		public int amount { get; set; }

		public string charge_id { get; set; }

		public string reason { get; set; }

	}
}
