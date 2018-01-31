namespace Rhea.Ast.Nodes
{
	public interface IStatementWithBlock
	{
		Block Block
		{
			get;
			set;
		}
	}
}