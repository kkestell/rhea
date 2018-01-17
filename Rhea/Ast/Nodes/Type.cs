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
            var cType = Value;

            switch (Value)
            {
                case "int8":
                    cType = "int8_t";
                    break;
                case "int16":
                    cType = "int16_t";
                    break;
                case "int32":
                    cType = "int32_t";
                    break;
                case "int64":
                    cType = "int64_t";
                    break;
                case "uint8":
                    cType = "uint8_t";
                    break;
                case "uint16":
                    cType = "uint16_t";
                    break;
                case "uint32":
                    cType = "uint32_t";
                    break;
                case "uint64":
                    cType = "uint64_t";
                    break;
            }

            if (Pointer)
                cType = $"{cType}*";

            return cType;
        }
    }
}