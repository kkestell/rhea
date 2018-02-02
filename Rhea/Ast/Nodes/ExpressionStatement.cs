namespace Rhea.Ast.Nodes
{
	public class ExpressionStatement : Statement
	{
		public Expression Expression { get; set; }

		public override string ToString()
		{
			return $"{Expression}";
		}
	}
}
