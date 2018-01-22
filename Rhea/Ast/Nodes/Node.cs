using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Rhea.Ast.Nodes
{
    public abstract class Node
    {
        public ParserRuleContext Context { get; set; }

        public string Source => Context.Start.InputStream.GetText(new Interval(Context.Start.StartIndex, Context.Stop.StopIndex));
    }
}