using PdfTagger.Pat;
using PdfTagger.Pdf;
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
    public partial class formTest004 : Form
    {
        public formTest004()
        {
            InitializeComponent();
        }

        private void formTest004_Load(object sender, EventArgs e)
        {
            PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(@"C:\ProgramData\PdfTagger\Inbox\0000020444.pdf");
            pdf.DocCategory = "Invoice";
            pdf.DocID = "A60028776";

            PdfTagPatternStore store = PdfTagPatternFactory.GetStore(pdf);

            PdfTagExtractionResult result = store.Extract(pdf);

        }
    }
}
