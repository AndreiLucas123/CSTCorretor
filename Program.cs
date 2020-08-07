using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CSTCorretor
{
  class Program
  {
    static int Main(string[] args)
    {
      Console.WriteLine();
      Console.WriteLine("  Iniciando corretor CST de 04 para 06");
      Console.WriteLine();

      Console.WriteLine("  Insira o caminho dos xml\n");
      Console.Write("  ");
      var path = Console.ReadLine();


      try
      {
        int arquivos = 0;
        // Get the files
        var files = Directory.GetFiles(path);
        // Filter the files
        files = files.Where(f => f.EndsWith(".xml", true, CultureInfo.InvariantCulture)).ToArray();

        if (files.Length == 0)
        {
					Console.WriteLine();
					Console.WriteLine("  Nenhum arquivo encontrado no diretório selecionado");
					return 0;
        }

        // Process
        string text;
        for (int i = 0; i < files.Length; i++)
        {
          text = File.ReadAllText(files[i]);
          text = text.Replace("<CST>04</CST>", "<CST>06</CST>");
          File.WriteAllText(files[i], text);
          arquivos++;
          if (arquivos % 50 == 0)
          {
            Console.WriteLine("  " + arquivos + " xml formatados");
          }
        }
        Console.WriteLine("  " + arquivos + " xml formatados");
        Console.WriteLine();
        Console.WriteLine("  FINALIZADO COM SUCESSO");
      }
      catch (System.Exception ex)
      {
        Console.WriteLine("  Erro: " + ex.Message);
        Console.ReadKey();
        return 1;
      }
      Console.ReadKey();
      return 0;
    }


  }
}
