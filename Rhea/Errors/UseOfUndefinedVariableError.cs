using System;

namespace Rhea.Errors
{
    public class UseOfUndefinedVariableError : Exception
    {
        public UseOfUndefinedVariableError(string message) : base(message)
        {
        }
    }
}
