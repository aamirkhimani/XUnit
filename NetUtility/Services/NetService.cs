using System;
using NetUtility.DNS;
using NetUtility.Objects;

namespace NetUtility.Services
{
	public class NetService
	{
        private readonly IDNS _dNs;

        public NetService(IDNS dNs)
		{
            _dNs = dNs;
        }

		public string SendPing()
		{
			bool dNsResult = _dNs.SendDNS();

			if (dNsResult)
			{
                return "Success: Ping for IP:192.168.2.123 sent!";
            }
			else
			{
				return "Failed: An error occured";
			}
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

