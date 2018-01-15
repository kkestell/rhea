namespace Rhea.Ast.Nodes
{
    public class IfStatement : Scope
    {
        public Expression Expression { get; set; }

        public override string ToString()
        {
            return $"if({Expression})\n{Block}";
        }
    }
}