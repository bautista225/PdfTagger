using iTextSharp.text;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;

namespace PdfTagger.Pdf
{

    /// <summary>
    /// Extrategia de extracción de texto utilizada en el 
    /// método estático GetTextFromPage de la clase
    /// PdfTextExtractor. En esta clase que hereda de la
    /// clase LocationTextExtractionStrategy, sobreescribimos
    /// el método RenderText, para guardar la información 
    /// espacial de la situación del texto en los rectángulos
    /// de la colección PdfRectangles.
    /// </summary>
    public class PdfTextRectangleTextExtractionStrategy : LocationTextExtractionStrategy
    {
        /// <summary>
        /// Rectangulos con texto contenido de una página
        /// de documento pdf.
        /// </summary>
        public List<PdfTextRectangle> PdfRectangles { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PdfTextRectangleTextExtractionStrategy()
        {
            PdfRectangles = new List<PdfTextRectangle>();
        }

        /// <summary>
        /// Obtiene el texto contenido en un pdf en función del parámetro facilitado.
        /// </summary>
        /// <param name="renderInfo">Información para la obtención del texto.</param>
        public override void RenderText(TextRenderInfo renderInfo)
        {

            base.RenderText(renderInfo);

            var bottomLeft = renderInfo.GetDescentLine().GetStartPoint();
            var topRight = renderInfo.GetAscentLine().GetEndPoint();

            var rect = new Rectangle(bottomLeft[Vector.I1],
                                    bottomLeft[Vector.I2],
                                    topRight[Vector.I1],
                                    topRight[Vector.I2]);

            PdfRectangles.Add(new PdfTextRectangle()
            {
                Rectangle = rect,
                Text = renderInfo.GetText()
            });

        }
    }
}
