using System;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    public class MemberAccess : Atom
    {
        public string VariableName { get; set; }

        public string MemberName { get; set; }

public override Type InferredType
{
    get
    {
        var variableDeclaration = ParentBlock.FindDeclaration(VariableName);

        if(variableDeclaration == null)
            throw new Exception($"Can't find declaration for variable {VariableName}");

        var structDeclaration = ParentBlock.FindStruct(variableDeclaration.Type.Name);

        if(structDeclaration == null)
            throw new Exception($"Can't find a struct named {variableDeclaration.Type.Name}");

        var structMember = structDeclaration.Members.SingleOrDefault(m => m.Name == MemberName);

        if(structMember == null)
            throw new Exception($"Struct {structDeclaration.Name} has no member {MemberName}");

        return structMember.Type;
    }
}

        public override string ToString()
        {
            return $"{VariableName}.{MemberName}";
        }
    }
}