using org.pdfclown.documents.contents.colorSpaces;
using org.pdfclown.documents.contents.fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTagger.Pdf
{
    /// <summary>
    /// Texto contenido en una página
    /// de un documento PDF en el que se arrastran las propiedades como:
    /// -ColorFill
    /// -ColorStroke
    /// -FontType
    /// -FontSize
    /// </summary>
    public class PdfClownTextString
    {
        #region Constructors

        /// <summary>
        /// Construye una instancia de la clase PdfClownTextString 
        /// a partir de un texto extraido con el PdfClownTextExtractor junto a sus propiedades.
        /// </summary>
        /// <param name="text">Texto extraido con el PdfClownTextExtractor</param>
        /// <param name="colorFill">Color del texto</param>
        /// <param name="colorStroke">Color del texto</param>
        /// <param name="fontType">Tipo de fuente del texto</param>
        /// <param name="fontSize">Tamaño de fuente del texto</param>
        public PdfClownTextString(string text, Color colorFill, Color colorStroke, Font fontType, double fontSize)
        {
            Text = text;
            ColorFill = colorFill;
            ColorStroke = colorStroke;
            FontType = fontType;
            FontSize = fontSize;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Texto extraido con el PdfClownTextExtractor
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Color del texto
        /// </summary>
        public Color ColorFill { get; set; }

        /// <summary>
        /// Color del texto
        /// </summary>
        public Color ColorStroke { get; set; }

        /// <summary>
        /// Tipo de fuente del texto
        /// </summary>
        public Font FontType { get; set; }

        /// <summary>
        /// Tamaño de fuente del texto
        /// </summary>
        public double? FontSize { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Representación textual de la instancia tal que Hola [Font: Times-Roman, tam: 12] [colorFill {0,1,0}, colorStroke {1,0,0}]
        /// </summary>
        /// <returns>Representación tal que Hola [Font: Times-Roman, tam: 12] [colorFill {0,1,0}, colorStroke {1,0,0}]</returns>
        public override string ToString()
        {
            return Text +" [Font: " + FontType.Name + ", tam: " + Math.Round((double)FontSize) + "]" +
                                " [colorFill  " + ColorFill.BaseDataObject +
                                ", colorStroke " + ColorStroke.BaseDataObject.ToString() + "]\n";
        }

        #endregion
    }
}
