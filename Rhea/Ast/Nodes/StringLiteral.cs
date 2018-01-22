using System.Reflection;

namespace Rhea.Ast.Nodes
{
    public class StringLiteral : Expression
    {
        public string Value { get; set; }

        public override string ToString() => $"str(\"{Value}\")";

        public override Type InferredType => new Type("^string");
    }
}
