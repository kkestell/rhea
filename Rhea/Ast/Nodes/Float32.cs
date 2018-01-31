using System.Globalization;

namespace Rhea.Ast.Nodes
{
	public class Float32 : Number
	{
		public float Value
		{
			get;
			set;
		}

		public override Type InferredType
		{
			get => new Type("float32");
		}

		public override string ToString()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}
	}
}