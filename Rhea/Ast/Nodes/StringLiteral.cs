using System.Reflection;

namespace Rhea.Ast.Nodes
{
	public class StringLiteral : Expression
	{
		public string Value
		{
			get;
			set;
		}

		public override string ToString()
		{
			return $"string__new(\"{Value}\")";
		}

		public override Type InferredType
		{
			get => new Type("string");
		}
	}
}
