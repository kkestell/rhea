namespace Rhea.Ast.Nodes
{
	public class Assignment : Statement
	{
		public string VariableName
		{
			get;
			set;
		}

		public Expression Expression
		{
			get;
			set;
		}

		public override string ToString()
		{
			return $"{VariableName} = {Expression};";
		}
	}
}
