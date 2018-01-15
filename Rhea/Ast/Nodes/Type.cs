namespace Rhea.Ast.Nodes
{
    public class Type : Node
    {
        public Type(string type)
        {
            if (type.StartsWith("^"))
            {
                Value = type.TrimStart('^');
                Pointer = true;
            }
            else
            {
                Value = type;
            }
        }

        public string Value { get; set; }
        public bool Pointer { get; set; }

        public static bool operator ==(Type t1, Type t2)
        {
            return t1.Pointer == t2.Pointer && t1.Value == t2.Value;
        }

        public static bool operator !=(Type t1, Type t2)
        {
            return t1.Pointer != t2.Pointer || t1.Value != t2.Value;
        }

        public override string ToString()
        {
            if (Value == "string")
                return "char*";

            if (Pointer)
                return $"{Value}*";

            return Value;
        }
    }
}