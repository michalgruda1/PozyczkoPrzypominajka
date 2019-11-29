using System;

namespace PozyczkoPrzypominajkaV2.Services
{
	public class Environment : IEnvironment
	{
		private DateTime NowDateTime = DateTime.Now;

		public void SetNow(DateTime SomeTime)
		{
			this.NowDateTime = SomeTime;
		}

		public DateTime Now()
		{
			return this.NowDateTime;
		}
	}
}
