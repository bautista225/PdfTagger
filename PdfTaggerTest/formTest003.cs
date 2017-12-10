using PdfTagger.Dat;
using PdfTagger.Dat.Met.Bus;
using PdfTagger.Dat.Txt;
using PdfTagger.Pat;
using PdfTagger.Pdf;
using System;
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
            PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(@"C:\ProgramData\MagicInvoice\Outbox\0000017228.pdf");
            pdf.DocCategory = "Invoice";
            pdf.DocID = "A39454509";

            // y de un conjunto de datos estructurados
            InvoiceMetadata metadata = new InvoiceMetadata();

            metadata.InvoiceNumber = "338";
            metadata.BuyerPartyID = "A39454509";
            metadata.IssueDate = new DateTime(2017, 9, 7);
            metadata.GrossAmount = -78.09m;
            metadata.TaxesOutputsBase01 = -64.54m;
            metadata.TaxesOutputsRate01 = 21m;
            metadata.TaxesOutputsAmount01 = -13.55m;

            PdfCompareResult compareResult = PdfCompare.Compare(new BusinessHierarchySet(), pdf, metadata);

            PdfTagPatternFactory.Save(compareResult);


        }
    }
}
