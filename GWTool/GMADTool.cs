using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GWTool
{
    public static class GMADTool
    {
        public static int Extract(string gmaFile, string outputDir)
        {
            FileStream fs = new FileStream(gmaFile, FileMode.Open);
            BinaryReader reader = new BinaryReader(fs);

            uint i = reader.ReadUInt32();

            if (i != 0x44414d47)
            {
                // Invalid GMA file
                reader.Close();
                fs.Close();
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
            {
                reader.Close();
                fs.Close();
                return 2;
            }

            // Create the addons directory
            string addonDir = Path.Combine(outputDir, ScrubFileName(addonName));
            if (!Directory.Exists(addonDir))
                Directory.CreateDirectory(addonDir);

            foreach (object[] file in compressedFiles)
            {
                byte[] fileContent = reader.ReadBytes(Convert.ToInt32(file[1]));
                string fileDir = Path.Combine(addonDir, Path.GetDirectoryName((string)file[0]));
                string fileName = Path.GetFileName((string)file[0]);

                if (!Directory.Exists(fileDir))
                    Directory.CreateDirectory(fileDir);

                File.WriteAllBytes(Path.Combine(fileDir, fileName), fileContent);
            }

            string infoFilePath = Path.Combine(addonDir, "addon.txt");

            //Create the info file
            File.WriteAllText(infoFilePath, "\"AddonInfo\"\r\n{\r\n\t\"name\" \"" + addonName + "\"\r\n\t\"author_name\" \"" + addonAuthor + "\"\r\n\t\"info\" \"" + addonDesc + "\"\r\n}");

            reader.Close();
            fs.Close();

            return 0;
        }

        private static string ReadString(BinaryReader reader)
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

        public static string ScrubFileName(string value)
        {
            StringBuilder sb = new StringBuilder(value);
            char[] invalid = Path.GetInvalidFileNameChars();
            foreach (char item in invalid)
            {
                sb.Replace(item.ToString(), "");
            }
            return sb.ToString();
        }
    }
}
