namespace Rhea.Ast.Nodes
{
    class Type : Node
    {
        public string Value { get; set; }
        public bool Pointer { get; set; }

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
