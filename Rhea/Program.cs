using System;
using System.Diagnostics;
using System.IO;

namespace Rhea
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();

			var inputFilename = args[0];
			var outputFilename = $"{Path.GetFileNameWithoutExtension(inputFilename)}.c";

			var src = File.ReadAllText(inputFilename);

			var output = new Compiler().Compile(src);

			File.WriteAllText(outputFilename, output);

			Console.WriteLine(output);

			sw.Stop();
			Console.WriteLine(sw.Elapsed);
		}
	}
}