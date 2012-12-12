using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GWExtract
{
    public static class GMATool
    {
        public static int Extract(string gmaFile, string outputDir)
        {
            FileStream fs = new FileStream(gmaFile, FileMode.Open);
            BinaryReader reader = new BinaryReader(fs);

            uint i = reader.ReadUInt32();

            if (i != 0x44414d47)
            {
                // Invalid GMA file
                return 1;
            }

            fs.Seek(18, SeekOrigin.Current);

            string addonName = ReadString(reader);
            string addonDesc = ReadString(reader);
            string addonAuthor = ReadString(reader);

            fs.Seek(4, SeekOrigin.Current);

            // Store the information about all the compressed files
            List<object[]> compressedFiles = new List<object[]>();

            while (true)
            {
                uint filenum = reader.ReadUInt32();
                if (filenum == 0)
                    break;

                object[] file = new object[2];
                file[0] = ReadString(reader);
                file[1] = reader.ReadUInt32();

                compressedFiles.Add(file);
                fs.Seek(8, SeekOrigin.Current);
            }

            // If list is empty, the archive is empty
            if (compressedFiles.Count < 1)
                return 2;

            foreach (object[] file in compressedFiles)
            {
                byte[] fileContent = reader.ReadBytes((int)file[1]);
                string fileDir = Path.Combine(outputDir, Path.GetDirectoryName((string)file[0]));

                if (!Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                File.WriteAllBytes(fileDir, fileContent);
            }

            //Create the info file
            File.WriteAllText(Path.Combine(outputDir, "\\addon.txt"), 
                                "\"AddonInfo\"\r\n{\r\n\t\"name\" \"" + addonName + "\"\r\n\t\"author_name\" \"" + addonAuthor + "\"\r\n\t\"info\" \"" + addonDesc + "\"\r\n}");

            reader.Close();
            fs.Close();

            return 0;
        }

        static string ReadString(BinaryReader reader)
        {
            string result = string.Empty;

            while (true)
            {
                char Char = reader.ReadChar();

                if (Char == '\0')
                    break;

                result += Char;
            }

            return result;
        }
    }
}
