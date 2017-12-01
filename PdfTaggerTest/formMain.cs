using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PdfTagger.Pdf;
using PdfTagger.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTaggerTest
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {

            PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(@"C:\ProgramData\MagicInvoice\Outbox\0000019014.pdf");

            XmlParser.SaveAsXml(pdf, "C:\\zz.xml");

        }
    }
}
