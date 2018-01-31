using System;
using System.Collections.Generic;

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
					throw new Exception($"Can't find declaration for {Name}");

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