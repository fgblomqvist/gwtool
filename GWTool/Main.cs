using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
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
            
            // If raw Steam Workshop file (7z), extract GMA file

            // Else if GMA, extract addon folder

            int result = GMADTool.Extract(file, "C:\\");

            MessageBox.Show(result.ToString());
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
            LZMA
        }

        private FileType AnalyzeFile(string file)
        {
            // The format is stored in the first 4 bytes
            byte[] buffer = new byte[4];
            int format;

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer, 0, buffer.Length);
                format = BitConverter.ToInt32(buffer, 0);
            }

            switch (format)
            {
                case 0x5D0000:
                    return FileType.LZMA;

                case 0x474D4144:
                    return FileType.GMAD;

                case 0x44555033:
                    return FileType.DUPE;

                case 0x474D5333:
                    return FileType.GMS;
            }
        }
    }
}
