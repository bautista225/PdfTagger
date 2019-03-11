using org.pdfclown.documents.contents.colorSpaces;
using org.pdfclown.documents.contents.fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTagger.Pdf
{
    public class PdfClownTextString
    {
        #region Constructors
        
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

        public string Text { get; set; }

        public Color ColorFill { get; set; }

        public Color ColorStroke { get; set; }

        public Font FontType { get; set; }
        
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
