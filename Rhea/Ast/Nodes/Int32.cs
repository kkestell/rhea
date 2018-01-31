namespace Rhea.Ast.Nodes
{
	public class Int32 : Number
	{
		public int Value
		{
			get;
			set;
		}

		public override Type InferredType
		{
			get => new Type("int32");
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}