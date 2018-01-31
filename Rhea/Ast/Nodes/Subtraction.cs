using System;

namespace Rhea.Ast.Nodes
{
	public class Subtraction : Infix
	{
		public override Type InferredType
		{
			get
			{
				if (Left.InferredType != Right.InferredType)
				{
					throw new Exception(
						$"Types of left ({Left.InferredType}) and right ({Right.InferredType}) sides of infix expression must match");
				}

				return Left.InferredType;
			}
		}

		public override string ToString()
		{
			return $"({Left} - {Right})";
		}
	}
}