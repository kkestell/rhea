using System.Globalization;

namespace Rhea.Ast.Nodes
{
    public class Float64 : Number
    {
        public double Value
        {
        	get;
        	set;
        }

        public override Type InferredType
        {
        	get => new Type("float64");
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}