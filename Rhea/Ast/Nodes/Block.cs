using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
	public class Block : IScope
	{
		public Block ParentBlock
		{
			get;
			set;
		}

		public IEnumerable<Statement> Statements
		{
			get;
			set;
		}

		public override string ToString()
		{
			var statements = string.Join("\n", Statements.Select(e => e == null ? "" : e.ToString()));

			return $"{{\n{statements}\n}}";
		}

        #region IScope

		public IScope ParentScope
		{
			get;
			set;
		}

		public VariableDeclaration FindDeclaration(string name)
		{
			var declaration = Statements
			.OfType<VariableDeclaration>()
			.FirstOrDefault(d => d.Name == name);

			return declaration ?? ParentScope.FindDeclaration(name);
		}

		public Function FindFunction(string name)
		{
			return ParentScope.FindFunction(name);
		}

		public Struct FindStruct(string name)
		{
			return ParentScope.FindStruct(name);
		}

        #endregion
	}
}