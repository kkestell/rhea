namespace Rhea.Ast.Nodes
{
	public class Type : Node
	{
		public Type(string type)
		{
			if (type.StartsWith("^"))
			{
				Name = type.TrimStart('^');
				Pointer = true;
			}
			else
			{
				Name = type;
			}
		}

		public string Name
		{
			get;
			set;
		}

		public bool Pointer
		{
			get;
			set;
		}

		public static bool operator ==(Type t1, Type t2)
		{
			return t1?.Pointer == t2?.Pointer && t1?.Name == t2?.Name;
		}

		public static bool operator !=(Type t1, Type t2)
		{
			return t1?.Pointer != t2?.Pointer || t1?.Name != t2?.Name;
		}

		public override string ToString()
		{
			var cType = Name;
			var cPtr = Pointer;

			switch (Name)
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
				case "string":
				cType = "string";
				cPtr = true;
				break;
			}

			if (cPtr)
			cType = $"{cType}*";

			return cType;
		}
	}
}