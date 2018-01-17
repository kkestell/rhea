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
        public int Value { get; set; }

        public override Type InferredType => new Type("int32");

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class Int64 : Number
    {
        public long Value { get; set; }

        public override Type InferredType => new Type("int64");

        public override string ToString()
        {
            return $"{Value}L";
        }
    }

    public class Float32 : Number
    {
        public float Value { get; set; }

        public override Type InferredType => new Type("float32");

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public class Float64 : Number
    {
        public double Value { get; set; }

        public override Type InferredType => new Type("float64");

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}