using PdfTagger.Dat;
using PdfTagger.Dat.Met.Bus;
using PdfTagger.Dat.Txt;
using PdfTagger.Pat;
using PdfTagger.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTaggerTest
{

    /// <summary>
    /// Modelo para la vista del formulario formInvoices.
    /// </summary>
    public class formInvoiceModel
    {

        public string PdfPath { get; private set; }

        public PdfUnstructuredDoc Pdf { get; private set; }

        public InvoiceMetadata Invoice { get; set; }

        public PdfTagPatternStore Store { get; private set; }

        public PdfTagExtractionResult ExtractionResult { get; private set; }

        /// <summary>
        /// Carga un documento pdf.
        /// </summary>
        /// <param name="path">Documento pdfs a analizar.</param>
        public void LoadPdfInvoiceDoc(string path)
        {
            PdfPath = path;
            Pdf = new PdfUnstructuredDoc(PdfPath);
            Pdf.DocCategory = "Invoice";
        }

        /// <summary>
        /// Ejecuta el aprendizaje de patrones sobre unos
        /// metadatos de factura y datos leídos de pdf previos.
        /// </summary>
        public void ExecutePatternsLearning()
        {
            if (string.IsNullOrEmpty(Pdf.DocID))
                throw new InvalidOperationException("Es necesario un valor Pdf.DocID.");

            PdfCompareResult compareResult = PdfCompare.Compare(new BusinessHierarchySet(), Pdf, Invoice);
            PdfTagPatternFactory.Save(compareResult);
        }

        /// <summary>
        /// Intenta la extracción de datos mediante patrones
        /// aprendidos.
        /// </summary>
        public void Extract()
        {

            try
            {
                Store = PdfTagPatternFactory.GetStore(Pdf);
            }
            catch
            {
            }

            if (Store != null)
            {
                ExtractionResult = Store.Extract(Pdf);
                Invoice = ExtractionResult.Metadata as InvoiceMetadata;
            }
        }

    
    }
}
