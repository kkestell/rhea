using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
	using System.Text.RegularExpressions;

	public class Module : Node
	{
		public string Name
		{
			get;
		}

		public IEnumerable<Function> ExternFunctions
		{
			get;
			set;
		}

		public IEnumerable<Function> Functions
		{
			get;
			set;
		}

		public IEnumerable<Struct> Structs
		{
			get;
			set;
		}

		public Module ParentModule
		{
			get;
			set;
		}

		public Module(string name)
		{
			Name = name;
		}

		public override string ToString()
		{
			var standardIncludes = new[]
			{
				"rhea"
			};

			var standardIncludeStatements = string.Join(
				"\n",
				standardIncludes.Select(i => $"#include \"{i}.h\""));

			var externFunctionDeclarations = string.Join(
				"\n",
				ExternFunctions.Select(f => f.Declaration));

			var structs = string.Join(
				"\n",
				Structs
				.Select(s => s.ToString()));

			var forwardDeclarations = string.Join(
				"\n",
				Functions
				.Where(f => f.Name != "main")
				.Select(f => f.Declaration));

			var functionDefinitions = string.Join(
				"\n\n",
				Functions
				.Select(f => f.ToString()));

			var program = $"{standardIncludeStatements}\n\n{externFunctionDeclarations}\n\n{structs}\n\n{forwardDeclarations}\n\n{functionDefinitions}";

			program = Regex.Replace(program, @"\n{2,}", "\n\n").Trim();;

			return program;
		}

		public Function FindFunction(string name)
		{
			var func = Functions.SingleOrDefault(f => f.Name == name);

			if (func != null)
			return func;

			func = ExternFunctions.SingleOrDefault(f => f.Name == name);

			if (func != null)
			return func;

			if (ParentModule == null)
			return null;

			func = ParentModule.FindFunction(name);

			return func ?? null;
		}

		public Struct FindStruct(string name)
		{
			var s = Structs.FirstOrDefault(x => x.Name == name);

			if (s != null)
			return s;

			if (ParentModule == null)
			return null;

			s = ParentModule.FindStruct(name);

			return s ?? null;
		}
	}
}