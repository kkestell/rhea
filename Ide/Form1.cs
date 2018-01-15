using System;
using System.CodeDom.Compiler;
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
        Compiler compiler;

        public Form1()
        {
            InitializeComponent();
            compiler = new Compiler();
        }

        void Compile()
        {
            try
            {
                textBox2.Text = compiler.Compile(textBox1.Text).Replace("\n", "\r\n");
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
            }
        }

        void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Compile();
        }
    }
}
