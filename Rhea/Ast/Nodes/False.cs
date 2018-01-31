namespace Rhea.Ast.Nodes
{
	public class False : Atom
	{
		public override Type InferredType
		{
			get => new Type("bool");
		}

		public override string ToString()
		{
			return "false";
		}
	}
}
