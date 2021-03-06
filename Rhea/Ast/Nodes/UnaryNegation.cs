﻿namespace Rhea.Ast.Nodes
{
	public class UnaryNegation : Unary
	{
		public override Type InferredType
		{
			get => Expression.InferredType;
		}

		public override string ToString()
		{
			return $"(-{Expression})";
		}
	}
}