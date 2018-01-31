﻿using System;
using System.Linq;

namespace Rhea.Ast.Nodes
{
	public class MethodCall : FunctionCall
	{
		public Expression Receiver
		{
			get;
			set;
		}

		string FunctionName
		{
			get => $"{StructType}#{Name}";
		}

		string MangledName
		{
			get => $"{StructType}__{Name}";
		}

		string StructName
		{
			get => Receiver.InferredType.Name;
		}

		string StructType
		{
			get
			{
				var structDeclaration = ParentBlock.FindStruct(StructName);

				if(structDeclaration == null)
					throw new Exception($"Can't find declaration for struct {StructName}");

				return structDeclaration.Name;
			}
		}

		public override Type InferredType
		{
			get
			{
				var func = ParentBlock.FindFunction(FunctionName);

				if (func == null)
					throw new Exception($"Can't find declaration for {FunctionName}");

				return func.Type;
			}
		}

		public override string ToString()
		{
			var args = string.Empty;

			if (Arguments.Any())
				args = $", {string.Join(", ", Arguments)}";

			return $"{MangledName}({Receiver}{args})";
		}
	}
}