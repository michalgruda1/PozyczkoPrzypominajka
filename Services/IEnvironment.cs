using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Services
{
	public interface IEnvironment
	{
		public DateTime Now();
	}
}
