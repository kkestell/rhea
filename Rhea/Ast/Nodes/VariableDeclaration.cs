﻿namespace Rhea.Ast.Nodes
{
	public class VariableDeclaration : Statement
	{
		public string Name
		{
			get;
			set;
		}

		public Type Type
		{
			get;
			set;
		}

		public override string ToString()
		{
			return $"{Type} {Name};";
		}
	}
}