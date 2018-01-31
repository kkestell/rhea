namespace Rhea.Ast.Nodes
{
	public interface IScope
	{
		IScope ParentScope
		{
			get;
			set;
		}

		VariableDeclaration FindDeclaration(string name);

		Function FindFunction(string name);

		Struct FindStruct(string name);
	}
}