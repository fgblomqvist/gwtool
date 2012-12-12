using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            // If raw Steam Workshop file (7z), extract GMA file

            // Else if GMA, extract addon folder

            int result = GMADTool.Extract(file, "C:\\");

            MessageBox.Show(result.ToString());
        }
    }
}
