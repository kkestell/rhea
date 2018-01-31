using System;

namespace Rhea.Ast.Nodes
{
	public abstract class Unary : Expression
	{
		public Expression Expression
		{
			get;
			set;
		}

		public override Type InferredType
		{
			get => throw new NotImplementedException();
		}

		public override string ToString()
		{
			return $"({Expression})";
		}
	}
}