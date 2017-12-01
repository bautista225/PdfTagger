using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;

namespace PdfTagger.Pdf
{

    /// <summary>
    /// Representa la información no estructurada
    /// contenida en un documento pdf.
    /// </summary>
    public class PdfUnstructuredDoc
    {

        /// <summary>
        /// Categoría de documento a la que pertenece el pdf.
        /// </summary>
        public string DocCategory { get; set; }

        /// <summary>
        /// ID del documento pdf.
        /// </summary>
        public string DocID {get; set; }

        /// <summary>
        /// Páginas del documento pdf con su información
        /// no estructurada.
        /// </summary>
        public List<PdfUnstructuredPage> PdfUnstructuredPages { get; private set; }

        /// <summary>
        /// Construye una nueva instancia de PdfUnstructuredDoc
        /// a partir del pdf cuya ruta se facilita como 
        /// </summary>
        public PdfUnstructuredDoc()
        {
        }

        /// <summary>
        /// Construye una nueva instancia de PdfUnstructuredDoc
        /// a partir del pdf cuya ruta se facilita como 
        /// parámetro.
        /// </summary>
        /// <param name="pdfPath">Ruta a archivo pdf.</param>
        public PdfUnstructuredDoc(string pdfPath)
        {

            PdfUnstructuredPages = new List<PdfUnstructuredPage>();

            using (PdfReader pdfReader = new PdfReader(pdfPath))
                GetPdfData(pdfReader);

        }

        /// <summary>
        /// Construye una nueva instancia de PdfUnstructuredDoc
        /// a partir del pdf cyuos datos binarios se facilitan como 
        /// parámetro.
        /// </summary>
        /// <param name="pdfFile">Datos binarios de archivo pdf.</param>
        public PdfUnstructuredDoc(byte[] pdfFile)
        {

            PdfUnstructuredPages = new List<PdfUnstructuredPage>();

            using (PdfReader pdfReader = new PdfReader(pdfFile))
                GetPdfData(pdfReader);
        }

        /// <summary>
        /// Obtiene la información no estructurada.
        /// </summary>
        /// <param name="pdfReader"></param>
        private void GetPdfData(PdfReader pdfReader)
        {
            for (int p = 1; p <= pdfReader.NumberOfPages; p++)
            {

                PdfTextRectangleTextExtractionStrategy rectangleStrategy =
                    new PdfTextRectangleTextExtractionStrategy();

                string pdfText = PdfTextExtractor.GetTextFromPage(pdfReader, p,
                  rectangleStrategy);

                PdfUnstructuredPages.Add(new PdfUnstructuredPage(rectangleStrategy.PdfRectangles, pdfText));

            }
        }

        /// <summary>
        /// Representación textual de una instancia.
        /// </summary>
        /// <returns>Representacion textual del 
        /// PdfUnstructuredDoc actual.</returns>
        public override string ToString()
        {
            return $"{DocCategory}:{DocID}";
        }

    }
}
