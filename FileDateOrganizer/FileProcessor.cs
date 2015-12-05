using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;


namespace FileDateOrganizer
{
  class FileProcessor
  {
    public DirectoryInfo SourcePath { get; private set; }
    public DirectoryInfo DestinationPath { get; private set; }

    public FileProcessor (DirectoryInfo sourcePath, DirectoryInfo destPath) {
      SourcePath = sourcePath;
      DestinationPath = destPath;
    }

    public List<FileInfo> MoveFilesToDateFolders()
    {
      List<FileInfo> files = SourcePath.EnumerateFiles().ToList();
      List<FileInfo> movedFiles = new List<FileInfo>();

      Console.WriteLine("Moving {2} file(s){0}Source:{1}{3}{0}Dest:{1}{4}",
        Environment.NewLine,
        "\t",
        files.Count(),
        SourcePath.FullName,
        DestinationPath.FullName);

      Console.Write("Press a key to continue...");
      Console.ReadLine();
      Console.WriteLine();

      foreach (FileInfo fileToMove in files.OrderBy(f => f.LastWriteTime))
      {
        DateTime fileDate = GetFileDate(fileToMove); 
        
        string dateDirectoryName = fileDate.ToString("yyyy-MM-dd");
        DirectoryInfo dateDirectory = DestinationPath.EnumerateDirectories().FirstOrDefault(d => d.Name == dateDirectoryName);

        //Create the directory if needed
        if(dateDirectory == null)
        {
          Console.WriteLine("Creating Directory {0}", dateDirectoryName);
          dateDirectory = DestinationPath.CreateSubdirectory(dateDirectoryName);
        }

        string newFilePath = String.Format(@"{0}\{1}", dateDirectory.FullName, fileToMove.Name);

        if (File.Exists(newFilePath))
        {
          Console.WriteLine("File Already Exists {0}", newFilePath);
        }
        else
        {
          Console.WriteLine("Moving File {0} to {1}", fileToMove.FullName, dateDirectory.FullName);
          fileToMove.MoveTo(newFilePath);
          movedFiles.Add(new FileInfo(newFilePath));
        }
      }

      return movedFiles;
    }

    private DateTime GetFileDate(FileInfo file)
    {
      DateTime result = file.LastWriteTime;
      FileStream fStream = null;

      //Try to get the date taken
      try
      {
        fStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        BitmapSource imageSource = BitmapFrame.Create(fStream);
        BitmapMetadata imageMetadata = (BitmapMetadata)imageSource.Metadata;

        string dateTaken = imageMetadata.DateTaken;

        if(dateTaken != null)
          DateTime.TryParse(dateTaken, out result);
      }
      catch
      {

      }
      finally
      {
        if (fStream != null)
          fStream.Close();
      }

      return result;

      }
  }
}
