using Rhea.Errors;

namespace Rhea.Ast.Nodes
{
	public class Variable : Atom
	{
		public string Name
		{
			get;
			set;
		}

		public override Type InferredType
		{
			get
			{
				var variableDeclaration = ParentBlock.FindDeclaration(Name);

				if(variableDeclaration == null)
					throw new UseOfUndefinedVariableError($"Can't find declaration for {Name}");

				return variableDeclaration.Type;
			}
		}

		public override string ToString()
		{
			return Name;
		}
	}
}