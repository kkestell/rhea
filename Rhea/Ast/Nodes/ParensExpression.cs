﻿namespace Rhea.Ast.Nodes
{
	class ParensExpression : Expression
	{
		public Expression Expression
		{
			get;
			set;
		}

		public override Type InferredType
		{
			get => Expression.InferredType;
		}

		public override string ToString()
		{
			return $"({Expression})";
		}
	}
}