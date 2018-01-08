using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Rhea;
using Rhea.Ast;

namespace Ide
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Compile()
        {
            try
            {
                var input = new AntlrInputStream(textBox1.Text);

                var lexer = new RheaLexer(input);
                var tokens = new CommonTokenStream(lexer);

                var parser = new RheaParser(tokens);
                var tree = parser.program();

                var astBuilder = new AstBuilder();
                ParseTreeWalker.Default.Walk(astBuilder, tree);

                textBox2.Text = astBuilder.ToString().Replace("\n", "\r\n");
            }
            catch (Exception ex)
            {
            }
        }

        void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Compile();
        }
    }
}
