namespace Rhea.Ast.Nodes
{
    class ReturnStatementNode : StatementNode
    {
        public ExpressionNode Expression { get; set; }

        public override string ToString()
        {
            return $"return {Expression};";
        }
    }
}
