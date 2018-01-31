namespace Rhea.Ast.Nodes
{
	public class Int64 : Number
	{
		public long Value
		{
			get;
			set;
		}

		public override Type InferredType
		{
			get => new Type("int64");
		}

		public override string ToString()
		{
			return $"{Value}L";
		}
	}
}