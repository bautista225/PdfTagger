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
            PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(@"C:\Users\Txema\Desktop\Inbox\0000014798.pdf");

            // y de un conjunto de datos estructurados
            InvoiceMetadata metadata = new InvoiceMetadata();

            metadata.InvoiceNumber = "REX1700028";
            metadata.BuyerPartyID = "ES - A12070330";
            metadata.IssueDate = new DateTime(2017, 11, 7);
            metadata.GrossAmount = -406.56m;
            metadata.TaxesOutputsBase01 = -336m;
            metadata.TaxesOutputsRate01 = 21m;
            metadata.TaxesOutputsAmount01 = -70.56m;

            PdfCompareResult compareResult = PdfCompare.Compare(new BusinessHierarchySet(), pdf, metadata);
          

        }
    }
}
