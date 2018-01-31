namespace Rhea.Ast.Nodes
{
	public class True : Atom
	{
		public override Type InferredType
		{
			get => new Type("bool");
		}

		public override string ToString()
		{
			return "true";
		}
	}
}
