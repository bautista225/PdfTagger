using PdfTagger.Dat;
using PdfTagger.Dat.Met.Bus;
using PdfTagger.Dat.Txt;
using PdfTagger.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTaggerTest
{
    public partial class formTest003 : Form
    {
        public formTest003()
        {
            InitializeComponent();
        }

        private void formTest003_Load(object sender, EventArgs e)
        {


            // Partiendo de una entrada de datos no estructurados de pdf 
            PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(@"C:\ProgramData\PdfTagger\Inbox\0000021101.pdf");

            // y de un conjunto de datos estructurados
            InvoiceMetadata metadata = new InvoiceMetadata();

            metadata.InvoiceNumber = "1 / 33050";
            metadata.BuyerPartyID = "ES - A12070330";
            metadata.IssueDate = new DateTime(2017, 11, 30);
            metadata.GrossAmount = 3646.50m;
            metadata.TaxesOutputsBase01 = 3013.64m;
            metadata.TaxesOutputsRate01 = 21m;
            metadata.TaxesOutputsAmount01 = 632.86m;

            PdfCompareResult compareResult = PdfCompare.Compare(new BusinessHierarchySet(), pdf, metadata);
          

        }
    }
}
