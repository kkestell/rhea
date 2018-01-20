using System.Collections.Generic;

namespace Rhea.Ast.Nodes
{
    public class Struct
    {
        public string Name { get; set; }
        
        public IEnumerable<Member> Members { get; set; }

        public override string ToString()
        {
            var members = string.Join("\n", Members);
            return $"typedef struct {{\n{members}\n}} {Name};";
        }
    }
}
