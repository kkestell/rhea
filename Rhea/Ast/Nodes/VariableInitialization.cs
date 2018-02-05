namespace Rhea.Ast.Nodes
{
	public class VariableInitialization : VariableDeclaration
	{
		public Expression Expression
		{
			get;
			set;
		}

		public new Type Type
		{
			get => Expression.InferredType;
		}

		public override string ToString()
		{
			return $"{Type} {Name} = {Expression};";
		}
	}
}