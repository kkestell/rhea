using System;

namespace Rhea.Ast.Nodes
{
	public class Range : Expression
	{
		public Expression Start
		{
			get;
			set;
		}

		public Expression End
		{
			get;
			set;
		}

		public override Type InferredType
		{
			get
			{
				if (Start.InferredType != End.InferredType)
				{
					throw new Exception(
						$"Types of start ({Start.InferredType}) and end ({End.InferredType}) of range expression must match");
				}

				return Start.InferredType;
			}
		}
	}
}
