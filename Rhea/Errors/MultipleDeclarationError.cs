using System;

namespace Rhea.Errors
{
	public class MultipleDeclarationError : Exception
	{
		public MultipleDeclarationError(string message) : base(message)
		{
		}
	}
}
