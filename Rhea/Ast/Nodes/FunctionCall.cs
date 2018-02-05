using System.Collections.Generic;
using System.Linq;

using Rhea.Errors;

namespace Rhea.Ast.Nodes
{
	public class FunctionCall : Expression
	{
		public string Name
		{
			get;
			set;
		}

		public IEnumerable<Expression> Arguments
		{
			get;
			set;
		}

		public override Type InferredType
		{
			get
			{
				var func = ParentBlock.FindFunction(Name);

				if(func == null)
					throw new UseOfUndefinedFunctionError($"Can't find declaration for {Name}");

				if (func.Parameters.Count() != Arguments.Count())
					throw new ArgumentError($"Function {func.Name} takes {func.Parameters.Count()} parameters, but {Arguments.Count()} were provided.");

				for (var i = 0; i < Arguments.Count(); i++)
				{
					var paramName = func.Parameters.ElementAt(i).Name;
					var paramType = func.Parameters.ElementAt(i).Type;
					var argType = Arguments.ElementAt(i).InferredType;

					if (paramType != argType)
						throw new TypeError($"Argument {paramName} to {func.Name} must be a {paramType}, not a {argType}");
				}

				return func.Type;
			}
		}

		string MangledName
		{
			get => Name.Replace("#", "__");
		}

		public override string ToString()
		{
			return $"{MangledName}({string.Join(", ", Arguments)})";
		}
	}
}