using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Exceptions
{
	public class AccessDeniedException : Exception
	{
		public AccessDeniedException(string message) : base(message)
		{

		}
	}
}
