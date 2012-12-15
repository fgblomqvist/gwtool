using System;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;

namespace GWTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            // Store all the filenames in an array
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);

            // Analyze & Extract them one by one
            foreach (string file in files)
            {
                HandleFile(file);
            }
        }

        private void HandleFile(string file)
        {
            // TODO: Add support for dropping folders

            // Analyze file and find out what type it is
            FileType type = AnalyzeFile(file);

            lblResult.Text = "The file is a " + type.ToString() + " file!" + "\r\n";
            
            // If raw Steam Workshop file (7z), extract GMA file

            // Else if GMA, extract addon folder

            //int result = GMADTool.Extract(file, "C:\\");

            //MessageBox.Show(result.ToString());
        }

        private enum FileType
        {
            // GMOD Addon
            GMAD,

            // GMOD Save
            GMS,

            // GMOD Dupe
            DUPE,

            // 7z Archive
            LZMA,

            Uknown
        }

        private FileType AnalyzeFile(string file)
        {
            // The format is stored in the first 4 bytes
            byte[] buffer = new byte[4];
            string hex;

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer, 0, buffer.Length);

                SoapHexBinary shb = new SoapHexBinary(buffer);
                hex = shb.ToString();
            }

            switch (hex)
            {
                case "5D0000":
                    return FileType.LZMA;

                case "474D4144":
                    return FileType.GMAD;

                case "44555033":
                    return FileType.DUPE;

                case "474D5333":
                    return FileType.GMS;

                default:
                    return FileType.Uknown;
            }
        }
    }
}
