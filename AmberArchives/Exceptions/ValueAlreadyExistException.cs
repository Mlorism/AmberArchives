using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Exceptions
{
	public class ValueAlreadyExistException : Exception
	{
		public ValueAlreadyExistException (string message) : base(message)
		{

		}
	}
}
