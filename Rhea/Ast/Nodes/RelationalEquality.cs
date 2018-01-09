namespace Rhea.Ast.Nodes
{
    class RelationalEquality : InfixExpression
    {
        public override string ToString()
        {
            return $"({Left} == {Right})";
        }
    }
}
