using System;
using NetUtility.Objects;

namespace NetUtility.Services
{
	public class NetService
	{
		public NetService()
		{
		}

		public string Ping()
		{
			return "Success: Ping 192.168.2.123!";
		}

		public int PingTimeout(int time, int space)
		{
			return time + space;
		}

		public DateTime LastPingDate()
		{
			return DateTime.Now;
		}

		public PingOptions GetPingOptions()
		{
			return new PingOptions()
			{
				IsAvailable = true,
				Ttl = 1
			};
		}

		public IEnumerable<PingOptions> GetPingOptionsList()
		{
			IEnumerable<PingOptions> pingOptionsList = new[]
			{
				new PingOptions()
				{
					IsAvailable = true,
					Ttl = 1
				},

                new PingOptions()
                {
                    IsAvailable = true,
                    Ttl = 2
                },
                new PingOptions()
                {
                    IsAvailable = true,
                    Ttl = 1
                },
                new PingOptions()
                {
                    IsAvailable = true,
                    Ttl = 2
                },
            };

			return pingOptionsList;
		}
	}
}

