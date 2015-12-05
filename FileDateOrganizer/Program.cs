using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileDateOrganizer
{
  class Program
  {
    static void Main(string[] args)
    {
      List<FileInfo> movedFiles = new List<FileInfo>();

      try
      {

        string argSourceDir = null;
        string argDestDir = null;

        if(args.Length == 1)  {
          argSourceDir = Environment.CurrentDirectory;
          argDestDir = args[0];
        }
        else if (args.Length == 2) {
          argSourceDir = args[0];
          argDestDir = args[1];
        }

        if(!Directory.Exists(argSourceDir))
        {
          throw new DirectoryNotFoundException(argSourceDir);
        }

        if(!Directory.Exists(argDestDir))
        {
          throw new DirectoryNotFoundException(argDestDir);
        }

        DirectoryInfo sourceDir = new DirectoryInfo(argSourceDir);
        DirectoryInfo destDir = new DirectoryInfo(argDestDir);

        FileProcessor fileProcessor = new FileProcessor(sourceDir, destDir);

        movedFiles = fileProcessor.MoveFilesToDateFolders();
      }
      catch  (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
      }
      finally
      {
        
      }

      Console.WriteLine("{0}{1} File(s) moved successfully", 
        Environment.NewLine,
        movedFiles.Count());
      Console.ReadLine();

      return;

      
    }
  }
}
