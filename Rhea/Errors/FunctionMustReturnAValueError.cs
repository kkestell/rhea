using System;

namespace Rhea.Errors
{
	public class FunctionMustReturnAValueError : Exception
	{
		public FunctionMustReturnAValueError(string message) : base(message)
		{
		}
	}
}
