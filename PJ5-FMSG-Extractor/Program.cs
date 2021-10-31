using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PJ5_FMSG_Extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "PJ5 FMSG Extractor by LeHieu - VietHoaGame";
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "-e":
                        if (args.Length >= 3)
                        {
                            if (isDirectory(args[1]))
                            {
                                string[] files = Directory.GetFiles(args[1], "*.fmsg", SearchOption.TopDirectoryOnly);
                                if (!Directory.Exists(args[2])) Directory.CreateDirectory(args[2]);
                                foreach (string file in files)
                                {
                                    FMSG.Extract(file, Path.Combine(args[2], $"{Path.GetFileName(file)}.txt"));
                                }
                            }
                            else FMSG.Extract(args[1], args[2]);
                        }
                        else
                        {
                            Console.WriteLine("-> Type \"-e [Input File/Folder] [Output File/Folder]\" to extract the file/folder.");
                        }
                        break;
                    case "-i":
                        if (args.Length >= 4)
                        {
                            if (isDirectory(args[1]))
                            {
                                string[] files = Directory.GetFiles(args[1], "*.fmsg", SearchOption.TopDirectoryOnly);
                                if (!Directory.Exists(args[3])) Directory.CreateDirectory(args[3]);
                                foreach (string file in files)
                                {
                                    if (File.Exists(Path.Combine(args[2], $"{Path.GetFileName(file)}.txt")))
                                    {
                                        FMSG.Import(file, Path.Combine(args[2], $"{Path.GetFileName(file)}.txt"), Path.Combine(args[3], $"{Path.GetFileName(file)}"));
                                    }
                                }
                            }
                            else FMSG.Import(args[1], args[2], args[3]);
                        }
                        else
                        {
                            Console.WriteLine("-> Type \"-i [Original File/Folder] [Input File/Folder] [Output File/Folder]\" to re-import the file/folder.");
                        }
                        break;
                    default:
                        Help();
                        break;
                }
            }
            else
            {
                Help();
            }
        }
        static bool isDirectory(string input)
        {
            FileAttributes attr = File.GetAttributes(input);
            return attr.HasFlag(FileAttributes.Directory);
        }
        static void Help()
        {
            Console.WriteLine("Usage:\n-> Type \"-e [Input File/Folder] [Output File/Folder]\" to extract the file/folder.\n-> Type \"-i [Original File/Folder] [Input File/Folder] [Output File/Folder]\" to re-import the file/folder.");
        }
    }
}
