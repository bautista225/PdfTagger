using PdfTagger.Dat;
using PdfTagger.Dat.Met.Bus;
using PdfTagger.Dat.Txt;
using PdfTagger.Pat;
using PdfTagger.Pdf;
using System;
using System.IO;
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
            PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(@"C:\ProgramData\PdfTagger\Inbox\0000020443.pdf");
            pdf.DocCategory = "Invoice";
            pdf.DocID = "A60028776";

            // y de un conjunto de datos estructurados
            InvoiceMetadata metadata = new InvoiceMetadata();

            metadata.InvoiceNumber = "11E179910000175042";
            metadata.SellerPartyID = "A60028776";
            metadata.BuyerPartyID = "A07529233";
            metadata.IssueDate = new DateTime(2017, 12, 1);
            metadata.GrossAmount = 381.80m;
            metadata.TaxesOutputsBase01 = 315.54m;
            metadata.TaxesOutputsRate01 = 21m;
            metadata.TaxesOutputsAmount01 = 66.26m;

            PdfCompareResult compareResult = PdfCompare.Compare(new BusinessHierarchySet(), pdf, metadata);

            PdfTagPatternFactory.Save(compareResult);


        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            dlgOpen.ShowDialog();

            if (File.Exists(dlgOpen.FileName))
            {

                PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(dlgOpen.FileName);                

               

                // y de un conjunto de datos estructurados
                InvoiceMetadata metadata = new InvoiceMetadata();

                metadata.InvoiceNumber = "11E179910000157660";
                metadata.SellerPartyID = "A60028776";
                metadata.BuyerPartyID = "A12650818";
                metadata.IssueDate = new DateTime(2017, 11, 1);
                metadata.GrossAmount = 1187.80m;
                metadata.TaxesOutputsBase01 = 981.64m;
                metadata.TaxesOutputsRate01 = 21m;
                metadata.TaxesOutputsAmount01 = 206.16m;

                pdf.DocCategory = "Invoice";
                pdf.DocID = metadata.SellerPartyID;

                PdfCompareResult compareResult = PdfCompare.Compare(new BusinessHierarchySet(), pdf, metadata);

                PdfTagPatternFactory.Save(compareResult);


            }
        }
    }
}
