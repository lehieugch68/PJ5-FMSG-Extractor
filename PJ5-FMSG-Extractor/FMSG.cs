using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PJ5_FMSG_Extractor
{
    public static class FMSG
    {
        private struct Header
        {
            public int Magic;
            public int StrCount;
            public int HeaderSize;
            public int TextOffset;
        }
        private static Header ReadHeader(ref BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            Header header = new Header();
            header.Magic = reader.ReadInt32();
            if (header.Magic != 0x47534D46) throw new Exception("Unsupported file type.");
            reader.BaseStream.Position += 4;
            header.StrCount = reader.ReadInt32();
            header.HeaderSize = reader.ReadInt32();
            header.TextOffset = reader.ReadInt32();
            return header;
        }
        public static void Extract(string input, string output)
        {
            List<string> result = new List<string>();
            using (Stream stream = File.OpenRead(input))
            {
                BinaryReader reader = new BinaryReader(stream);
                Header header = ReadHeader(ref reader);
                reader.BaseStream.Position = header.TextOffset;
                for (int i = 0; i < header.StrCount; i++)
                {
                    int textLen = reader.ReadInt32();
                    int blockLen = reader.ReadInt32();
                    int strSize = (textLen * 2) - 2;
                    string str = Encoding.Unicode.GetString(reader.ReadBytes(strSize)).Replace("\n", "{LF}");
                    int zeroes = blockLen - (8 + strSize);
                    reader.BaseStream.Position += zeroes;
                    result.Add(str);
                }
                reader.Close();
            }
            File.WriteAllLines(output, result);
            Console.WriteLine($"Extracted: {Path.GetFileName(output)}");
        }
        public static void Import(string original, string input, string output)
        {
            MemoryStream result = new MemoryStream();
            string[] text = File.ReadAllLines(input);
            using (BinaryWriter writer = new BinaryWriter(result))
            {
                using (Stream stream = File.OpenRead(original))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    Header header = ReadHeader(ref reader);
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    writer.Write(reader.ReadBytes(header.TextOffset));
                    for (int i = 0; i < header.StrCount && i < text.Length && reader.BaseStream.Position < reader.BaseStream.Length; i++)
                    {
                        int textLen = reader.ReadInt32();
                        int blockLen = reader.ReadInt32();
                        int strSize = (textLen * 2) - 2;
                        reader.BaseStream.Position += strSize;
                        int zeroes = blockLen - (8 + strSize);
                        reader.BaseStream.Position += zeroes;

                        text[i] = text[i].Replace("{LF}", "\n");
                        writer.Write(text[i].Length + 1);
                        byte[] newStrBytes = Encoding.Unicode.GetBytes(text[i]);
                        int newBlockLen = 8 + newStrBytes.Length + zeroes;
                        writer.Write(newBlockLen);
                        writer.Write(newStrBytes);
                        writer.Write(new byte[zeroes]);
                    }

                    if (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        writer.Write(reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position)));
                    }
                }
            }
            File.WriteAllBytes(output, result.ToArray());
            Console.WriteLine($"Imported: {Path.GetFileName(output)}");
        }
    }
}
