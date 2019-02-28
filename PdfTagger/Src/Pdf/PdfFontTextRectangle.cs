using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTagger.Pdf
{
    /// <summary>
    /// Rectángulo de una página PDF con información 
    /// como el texto contenido y la fuente
    /// </summary>
    public class PdfFontTextRectangle : PdfTextRectangle
    {

        #region Constructors

        /// <summary>
        /// Construye una instancia de la clase PdfFontTextRectangle
        /// </summary>
        public PdfFontTextRectangle() : base()
        {
        }

        /// <summary>
        /// Construye una nueva instancia de la clase iTextSharp.text.Rectangle
        /// a partir de un rectangulo de itext.
        /// </summary>
        /// <param name="iTextRectangle"></param>
        public PdfFontTextRectangle(iTextSharp.text.Rectangle iTextRectangle) : base(iTextRectangle)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Fuente del texto contenido en un rectángulo
        /// </summary>
        public string TextFont { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Representación textual de la instancia
        /// </summary>
        /// <returns>Representación textual de la instancia</returns>
        public override string ToString()
        {
            return base.ToString() + $"= '{TextFont}'";
        }

        #endregion

    }
}
