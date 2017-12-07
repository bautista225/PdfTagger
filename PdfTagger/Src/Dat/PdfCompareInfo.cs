using PdfTagger.Dat.Txt;
using PdfTagger.Pdf;
using System.Reflection;

namespace PdfTagger.Dat
{

    /// <summary>
    /// Resultado positivo de una búsqueda en un pdf.
    /// </summary>
    public class PdfCompareInfo
    {

        #region Private Members Variables

        PdfUnstructuredDoc _Pdf;
        PdfUnstructuredPage _PdfPage;
        PdfTextRectangle _PdfTextRectangle;
        ITextParserMatch _TextParserMatch;
        PropertyInfo _PropertyInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Construye una nueva instancia de la clase PdfCompareInfo.
        /// </summary>
        /// <param name="pdf">Instacia de la clase PdfUnstructuredDoc
        /// que se utilizó para la comparación a la que pertenence
        /// el info.</param>
        /// <param name="pdfPage">Instancia de la clase PdfUnstructuredPage
        /// de la colección PdfUnstructuredPages del pdf, sobre
        /// la que se obtuvo el resultado contenido en el
        /// info a crear.</param>
        /// <param name="pdfTextRectangle">PdfTextRectangle sobre el que
        /// se ha obetnido el resultado contenido en el info.</param>
        /// <param name="textParserMatch">ITextParserMatch orígen del info.</param>
        /// <param name="propertyInfo">PropetyInfo de la propiedad
        /// de los metadatos de la cual se a comparado el valor y se
        /// ha obtenido la coincidencia que ha generado el info.</param>
        public PdfCompareInfo(PdfUnstructuredDoc pdf,
            PdfUnstructuredPage pdfPage,
            PdfTextRectangle pdfTextRectangle,
            ITextParserMatch textParserMatch,
            PropertyInfo propertyInfo)
        {
            _Pdf = pdf;
            _PdfPage = pdfPage;
            _PdfTextRectangle = pdfTextRectangle;
            _TextParserMatch = textParserMatch;
            _PropertyInfo = propertyInfo;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Número de página del pdf de la que
        /// se ha obtenido la coincidencia orígen del
        /// info.
        /// </summary>
        public int PdfPageN
        {
            get
            {
                return _Pdf.PdfUnstructuredPages.IndexOf(_PdfPage);
            }

        }

        /// <summary>
        /// Nombre del item de metadatos del cual
        /// se ha utilizado el valor para buscar la
        /// coincidendia encontrada en el pdf que da
        /// orígen a esta instancia de info.
        /// </summary>
        public string MetadataItemName
        {
            get
            {
                return _PropertyInfo.Name;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Devuelve una representación textual
        /// de la intancia actual de PdfCompareInfo.
        /// </summary>
        /// <returns>Representación textual 
        /// de la intancia actual</returns>
        public override string ToString()
        {
            return $"({PdfPageN}) {_PropertyInfo.Name}: [{_TextParserMatch}]";
        }

        #endregion

    }
}
