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
using System.IO;
using System.Diagnostics;

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

        string Compile()
        {
            return compiler.Compile(textBox1.Text).Replace("\n", "\r\n");
        }

        void CompileAndUpdate()
        {
            try
            {
                textBox2.Text = Compile();
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
            }
        }

        void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        void Log(string message)
        {
            textBox3.AppendText($"{message}\r\n");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();

            var cSource = Compile();

            var sourceFilename = Path.ChangeExtension(Path.GetTempFileName(), "c");
            File.WriteAllText(sourceFilename, cSource);

            var tccPath = @"C:\TCC\tcc.exe";

            var outputFilename = Path.ChangeExtension(Path.GetTempFileName(), "exe");

            var compilerProcess = new Process();
            compilerProcess.StartInfo.FileName = tccPath;
            compilerProcess.StartInfo.Arguments = $"-o {outputFilename} {sourceFilename}";
            compilerProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            compilerProcess.StartInfo.UseShellExecute = false;
            compilerProcess.StartInfo.RedirectStandardOutput = true;
            compilerProcess.StartInfo.RedirectStandardError = true;
            compilerProcess.Start();
            compilerProcess.WaitForExit();

            var standardOutput = compilerProcess.StandardOutput.ReadToEnd();
            var standardError = compilerProcess.StandardError.ReadToEnd();

            if(compilerProcess.ExitCode != 0)
            {
                textBox2.Text = standardError;
                return;
            }

            sw.Stop();
            Log($"Compile finished in {sw.Elapsed}");

            var programProcess = new Process();
            programProcess.StartInfo.FileName = outputFilename;
            programProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            programProcess.StartInfo.UseShellExecute = false;
            programProcess.StartInfo.RedirectStandardOutput = true;
            programProcess.StartInfo.RedirectStandardError = true;
            programProcess.Start();
            programProcess.WaitForExit();

            standardOutput = programProcess.StandardOutput.ReadToEnd();
            standardError = programProcess.StandardError.ReadToEnd();

            Log($"Process returned {programProcess.ExitCode}");
        }

        private void fibonaciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var source = @"fun fib(n : int64) -> int64 {
  if(n == 0) {
    return 0
  }
  if(n == 1) {
    return 1
  }
  return fib(n - 2) + fib(n - 1)
}

fun main() -> int64 {
  return fib(10)
}";

            textBox1.Text = source;
            CompileAndUpdate();
        }

        private void helloWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var source = @"fun main() -> int64 {
  return 0
}";

            textBox1.Text = source;
            CompileAndUpdate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CompileAndUpdate();
        }
    }
}
