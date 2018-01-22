namespace Rhea.Ast.Nodes
{
    public class MemberAssignment : Statement
    {
        public string VariableName { get; set; }

        public string MemberName { get; set; }

        public Expression Expression { get; set; }

        public override string ToString()
        {
            return $"{VariableName}.{MemberName} = {Expression};";
        }
    }
}
