using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            FMSG.Extract(args[1], args[2]);
                        }
                        else
                        {
                            Console.WriteLine("-> Type \"-e [Input File] [Output File]\" to extract the file.");
                        }
                        break;
                    case "-i":
                        if (args.Length >= 4)
                        {
                            FMSG.Import(args[1], args[2], args[3]);
                        }
                        else
                        {
                            Console.WriteLine("-> Type \"-i [Original File] [Input File] [Output File]\" to re-import the file.");
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
            Console.ReadKey();
        }
        static void Help()
        {
            Console.WriteLine("Usage:\n-> Type \"-e [Input File] [Output File]\" to extract the file.\n-> Type \"-i [Original File] [Input File] [Output File]\" to re-import the file.");
        }
    }
}
