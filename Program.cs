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
      Console.WriteLine("  Iniciando corretor CST, CFOP e CSOSN");
      Console.WriteLine();
      Console.WriteLine("  Digite o modo:\n");
      Console.WriteLine("  1 - CST");
      Console.WriteLine("  2 - CST, CFOP e CSOSN\n");
      Console.Write("  ");

      int modo;
      var key = Console.ReadKey();
      if (key.Key == ConsoleKey.NumPad1)
      {
        modo = 1;
        Console.WriteLine(" - CST - SELECIONADO");
      }
      else if (key.Key == ConsoleKey.NumPad2)
      {
        modo = 2;
        Console.Write(" - CST, CFOP e CSOSN - SELECIONADO");
      }
      else
      {
        Console.WriteLine("\n  Tecla inválida");
        Console.ReadKey();
        return 1;
      }

      Console.WriteLine("\n\n  Insira o caminho dos xml\n");
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
          Console.ReadKey();
          return 0;
        }

        // Process
        string text;
        for (int i = 0; i < files.Length; i++)
        {
          text = File.ReadAllText(files[i]);
          if (modo == 1)
          {
            text = text.Replace("<CST>04</CST>", "<CST>06</CST>");
          }
          else
          {
            text = text.Replace("<CST>04</CST>", "<CST>01</CST>");
            text = text.Replace("<CSOSN>300</CSOSN>", "<CSOSN>500</CSOSN>");
            text = text.Replace("<CFOP>5102</CFOP>", "<CFOP>5405</CFOP>");
          }
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
