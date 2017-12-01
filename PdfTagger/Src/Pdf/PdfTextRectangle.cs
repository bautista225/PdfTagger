namespace PdfTagger.Pdf
{

    /// <summary>
    /// Rectangulo en una pagina de documento
    /// pdf con información en forma de texto.
    /// </summary>
    public class PdfTextRectangle
    {

        /// <summary>
        /// Rectangulo en una pagina de documento
        /// pdf.
        /// </summary>
        public iTextSharp.text.Rectangle Rectangle { get; set; }

        /// <summary>
        /// Texto dentro de el rectángulo.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{Rectangle} = '{Text}'";
        }

    }
}
