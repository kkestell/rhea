using System;

namespace Rhea.Errors
{
	public class UseOfUndefinedFunctionError : Exception
	{
		public UseOfUndefinedFunctionError(string message) : base(message)
		{
		}
	}
}
