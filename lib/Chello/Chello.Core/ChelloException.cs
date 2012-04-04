using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chello.Core
{
	public class ChelloException : Exception
	{
		public ChelloException(string message) : base(message) { }
		public ChelloException(string message, Exception innerException) : base(message, innerException) { }
	}
}