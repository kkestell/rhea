﻿using Rhea.Errors;

namespace Rhea.Ast.Nodes
{
	public class Addition : Infix
	{
		public override Type InferredType
		{
			get
			{
				if (Left.InferredType != Right.InferredType)
				{
					throw new TypeError($"Types of left ({Left.InferredType}) and right ({Right.InferredType}) sides of infix expression must match");
				}

				return Left.InferredType;
			}
		}

		public override string ToString()
		{
			return $"({Left} + {Right})";
		}
	}
}