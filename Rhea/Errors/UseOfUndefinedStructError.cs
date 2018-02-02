using System;

namespace Rhea.Errors
{
	public class UseOfUndefinedStructError : Exception
	{
        public UseOfUndefinedStructError(string message) : base(message)
        {
        }
    }
}
