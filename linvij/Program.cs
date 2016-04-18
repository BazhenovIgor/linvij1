using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Algo;
using Mono.Options;
namespace Vijineers
{
	class Program
	{
		static void Main(string[] args)
		{           
			string output = null;
			string  input = null;
			var show_help = false;
			var p = new OptionSet () {
				{ "i=", "The input file",   v => input = v  },
				{ "k=", "Specify the key file", v => output = v },
				"other:",
				{ "h|help",  "show this message", 
					v => show_help = v != null },
			};


			try {
				p.Parse (args);
			}
			catch (OptionException e)
			{
				Console.WriteLine (e.Message);
				return;
			}

			if (show_help) {
				p.WriteOptionDescriptions (Console.Out);
				return;
			}

			if (output == null|| input == null)
			{
				Console.WriteLine("Введите корректное название файлов; try --help");
				return;
			}
			Vijenera a = new Vijenera();	
			using (StreamReader sr = new StreamReader(input, System.Text.Encoding.Default))
			{
				StreamReader reader2 = new StreamReader(output);
				var key = reader2.ReadLine();
				reader2.Close();
				string line;
				StreamWriter SW = new StreamWriter(new FileStream( "out.txt", FileMode.Create, FileAccess.Write));
				SW.WriteLine("******************** key = {0}",  key);
				Console.WriteLine ("******************** key = {0}",  key);
				while ((line = sr.ReadLine()) != null)
				{	
					Console.WriteLine(line);
					SW.WriteLine(line);
					var result = a.Encryption(line, key, Vijenera.Option.First);
					Console.WriteLine(result);
					SW.WriteLine("After Encryption: {0}", result);
					result = a.Decryption(result, key, Vijenera.Option.First);
					Console.WriteLine("After Decryption: {0}", result);
					SW.WriteLine(result);
					Console.WriteLine("********************");
				}
				SW.Close();
			}
			Console.WriteLine("");
		}
	}
}