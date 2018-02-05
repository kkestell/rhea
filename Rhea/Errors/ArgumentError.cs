using System;

namespace Rhea.Errors
{
	public class ArgumentError : Exception
	{
		public ArgumentError(string message) : base(message)
		{
		}
	}
}