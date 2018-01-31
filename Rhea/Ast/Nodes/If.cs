namespace Rhea.Ast.Nodes
{
	public class If : Statement, IStatementWithBlock
	{
		public Expression Expression
		{
			get;
			set;
		}

		public override string ToString()
		{
			return $"if ({Expression}) {Block}";
		}

        #region IStatementWithBlock

		public Block Block
		{
			get;
			set;
		}

        #endregion
	}
}