using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfTagger.Pdf;
using PdfTagger.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTaggerTest
{
    public partial class formTest002 : Form
    {
        public formTest002()
        {
            InitializeComponent();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            dlgOpen.ShowDialog();
            if (File.Exists(dlgOpen.FileName))
            {

                
                string rectPath = $"{Path.GetDirectoryName(dlgOpen.FileName)}{System.IO.Path.DirectorySeparatorChar}" +
                    $"{Path.GetFileNameWithoutExtension(dlgOpen.FileName)}_rect.pdf";

                PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(dlgOpen.FileName);

                int p = 0;

                foreach (var page in pdf.PdfUnstructuredPages)
                {
                    p++;
                    foreach (var reg in page.WordGroups)
                        grdRegions.Rows.Add($"{page}", $"{reg}");
                    
                }

                Util.PrintRectangles(dlgOpen.FileName, rectPath, pdf);

            }
        }



    }
}
