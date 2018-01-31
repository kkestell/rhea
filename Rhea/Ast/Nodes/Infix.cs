namespace Rhea.Ast.Nodes
{
	public abstract class Infix : Expression
	{
		public Expression Left
		{
			get;
			set;
		}

		public Expression Right
		{
			get;
			set;
		}
	}
}