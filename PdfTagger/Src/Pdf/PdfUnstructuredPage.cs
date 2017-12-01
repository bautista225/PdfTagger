using System.Collections.Generic;

namespace PdfTagger.Pdf
{

    /// <summary>
    /// Representa una página de documento pdf
    /// con la información no estructurada obtenida
    /// mediante itextsharp.
    /// </summary>
    public class PdfUnstructuredPage
    {

        /// <summary>
        /// Construye una nueva instancia de PdfUnstructuredPage.
        /// </summary>
        public PdfUnstructuredPage()
        {
        }

        /// <summary>
        /// Construye una nueva instancia de PdfUnstructuredPage.
        /// </summary>
        /// <param name="pdfRectangles">Conjunto de rectangulos
        /// con información no estructurada en su interior.</param>
        /// <param name="pdfText">Texto total de la página.</param>
        public PdfUnstructuredPage(List<PdfTextRectangle> pdfRectangles, string pdfText)
        {
            PdfRectangles = pdfRectangles;
            PdfText = pdfText;
        }

        /// <summary>
        /// Rectangulos con información textual obtenidos
        /// de la página.
        /// </summary>
        public List<PdfTextRectangle> PdfRectangles { get; private set; }

        /// <summary>
        /// Informaciónno estructura total de la 
        /// página de pdf en forma de texto.
        /// </summary>
        public string PdfText { get; private set; }

        /// <summary>
        /// Representación textual de esta
        /// instancia de PdfUnstructuredPage.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{PdfText.Substring(0, 70)}...";
        }

    }
}
