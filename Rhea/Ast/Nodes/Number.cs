using System;
using System.Globalization;

namespace Rhea.Ast.Nodes
{
    public class Number : Atom
    {
        public override Type InferredType
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class Int32 : Number
    {
        public System.Int32 Value { get; set; }

        public override Type InferredType => new Type("int32");

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class Int64 : Number
    {
        public System.Int64 Value { get; set; }

        public override Type InferredType => new Type("int64");

        public override string ToString()
        {
            return $"{Value}L";
        }
    }

    public class Float32 : Number
    {
        public System.Single Value { get; set; }

        public override Type InferredType => new Type("float32");

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public class Float64 : Number
    {
        public System.Double Value { get; set; }

        public override Type InferredType => new Type("float64");

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}