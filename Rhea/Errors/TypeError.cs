using System;

namespace Rhea.Errors
{
	public class TypeError : Exception
	{
        public TypeError(string message) : base(message)
        {
        }
    }
}