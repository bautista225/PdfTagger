using PdfTagger.Dat;
using PdfTagger.Dat.Txt;
using PdfTagger.Pdf;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PdfTagger.Pat
{

    /// <summary>
    /// Almacén de patrones
    /// </summary>
    [Serializable]
    [XmlRoot("PdfPatternStore")]
    public class PdfTagPatternStore
    {

        #region Private Member Variables

        Dictionary<Type, dynamic> _Converters;

        #endregion

        #region Constructors

        /// <summary>
        /// Construye un nuevo almacén de patrones.
        /// </summary>
        public PdfTagPatternStore()
        {
            PdfPatterns = new List<PdfTagPattern>();
        }

        #endregion

        #region Public Properties


        /// <summary>
        /// Categoría de documento a la que pertenece el pdf.
        /// </summary>
        public string DocCategory { get; set; }

        /// <summary>
        /// ID del documento pdf.
        /// </summary>
        public string DocID { get; set; }

        /// <summary>
        /// Nombre del catálogo de jerarquías
        /// utilizado.
        /// </summary>
        public string HierarchySetName { get; set; }

        /// <summary>
        /// Clase que implementa la interfaz IMetadata
        /// asociada al resultado de identificación de
        /// patrones.
        /// utilizado.
        /// </summary>
        public string MetadataName { get; set; }

        /// <summary>
        /// Colección de patrones.
        /// </summary>
        public List<PdfTagPattern> PdfPatterns { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Ejecuta patrones los extracción de textos 
        /// almacenados.
        /// </summary>
        /// <param name="pdf"></param>
        /// <returns></returns>
        public PdfTagExtractionResult Extract(PdfUnstructuredDoc pdf)
        {

            PdfTagExtractionResult result = new PdfTagExtractionResult()
            {
                Pdf = pdf,
                MetadataType = Type.GetType(MetadataName)
            };

            _Converters = new Dictionary<Type, object>();

            IHierarchySet hierarchySet = GetHierarchySet();

            foreach (var page in pdf.PdfUnstructuredPages)
            {

                ExtractFromRectangles(page.WordGroups,
                    result.MetadataType, hierarchySet, result);

                ExtractFromRectangles(page.Lines,
                    result.MetadataType, hierarchySet, result, "LinesInfos");

                ExtractFromText(result.MetadataType, result, page);

            }

            result.Converters = _Converters;

            result.GetMetadata();

            return result;

        }

        /// <summary>
        /// Devuelve los patrones aprendidos para el sourceTypeName
        /// y el metadataItemName proporcionados como parámetros.
        /// </summary>
        /// <param name="sourceTypeName">WordGroupsInfos / LinesInfos ...</param>
        /// <param name="metadataItemName">Nombre de la propiedad de metadatos.</param>
        /// <returns></returns>
        public List<PdfTagPattern> GetPdfPatterns(string sourceTypeName, 
            string metadataItemName)
        {

            List<PdfTagPattern> pdfPatterns = new List<PdfTagPattern>();

            foreach (var patt in PdfPatterns)
                if (patt.SourceTypeName == sourceTypeName && 
                    patt.MetadataItemName == metadataItemName)
                    pdfPatterns.Add(patt);

            return pdfPatterns;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Ejecuata la extracción basada en limites
        /// textuales.
        /// </summary>
        /// <param name="metadataType">Tipo de la clase que implementa IMetadata.</param>
        /// <param name="result">Resultado de extracción.</param>
        /// <param name="page">PdfUnstructuredPage del doc. pdf.</param>
        private void ExtractFromText(Type metadataType, 
            PdfTagExtractionResult result, PdfUnstructuredPage page)
        {
            foreach (var pattern in PdfPatterns)
            {
                if (pattern.SourceTypeName == "PdfTextInfos")
                {
                    foreach (Match match in Regex.Matches(page.PdfText, pattern.RegexPattern))
                    {
                        PropertyInfo pInf = metadataType.GetProperty(pattern.MetadataItemName);

                        if (pInf.PropertyType == typeof(string))
                        {
                            result.AddResult(pattern.MetadataItemName, pattern.MatchesCount, match.Value);
                        }
                        else
                        {
                            dynamic converter = null;

                            if (_Converters.ContainsKey(pInf.PropertyType))
                            {
                                converter = _Converters[pInf.PropertyType];
                            }
                            else
                            {
                                Type converterGenType = typeof(Converter<>).MakeGenericType(pInf.PropertyType);
                                converter = Activator.CreateInstance(converterGenType);                                
                            }
                           
                            object pValue = converter.Convert(match.Value);
                            result.AddResult(pattern.MetadataItemName, pattern.MatchesCount, pValue);

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ejecuta el proceso de extracción de metadatos
        /// en base a los patrones almacenados.
        /// </summary>
        /// <param name="pdfDocRectangles">rectángulos del pdf doc.</param>
        /// <param name="metadataType">Implementa IMetadata.</param>
        /// <param name="hierarchySet">Catálogo de jerarquías.</param>
        /// <param name="result">Resultados.</param>
        /// <param name="sourceTypeName">Nombre de la fuente.</param>
        private void ExtractFromRectangles(List<PdfTextRectangle> pdfDocRectangles, 
            Type metadataType, IHierarchySet hierarchySet, PdfTagExtractionResult result,
            string sourceTypeName= "WordGroupsInfos")
        {
            foreach (var pdfDocRectangle in pdfDocRectangles)
            {
                foreach (var pattern in PdfPatterns)
                {
                    if (pattern.SourceTypeName == sourceTypeName)
                    {
                        if (IsAlmostSameArea(pdfDocRectangle, pattern.PdfRectangle))
                        {
                            string textInput = pdfDocRectangle.Text;
                            PropertyInfo pInf = metadataType.GetProperty(pattern.MetadataItemName);
                            ITextParserHierarchy parserHierarchy = hierarchySet.GetParserHierarchy(pInf.PropertyType);

                            if (pInf.PropertyType == typeof(string))
                                parserHierarchy.SetParserRegexPattern(0, pattern.RegexPattern);

                            dynamic converter = parserHierarchy.GetConverter(pattern.RegexPattern);

                            MatchCollection matches = Regex.Matches(pdfDocRectangle.Text, pattern.RegexPattern);

                            string val = (pattern.Position < matches.Count) ? 
                                matches[pattern.Position].Value : null;

                            object pValue = null;

                            if (val != null && converter != null)
                                pValue = converter.Convert(val);

                            if (pValue != null && !PdfCompare.IsZeroNumeric(pValue))
                            {
                                result.AddResult(pattern.MetadataItemName, pattern.MatchesCount, pValue);
                                if (!_Converters.ContainsKey(pInf.PropertyType))
                                    _Converters.Add(pInf.PropertyType, converter);
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Devuelve el coeficiente del área del rectángulo 
        /// intersección con el de la mayor área de los dos
        /// rectángulos de entrada.
        /// </summary>
        /// <returns>Coeficiente de área común. Un valor
        /// de 1 significa que ambos rectángulos son iguales.</returns>
        private float GetCommonAreaCoef(PdfTextBaseRectangle first, 
            PdfTextBaseRectangle second)
        {
            iTextSharp.text.Rectangle firstRect = new iTextSharp.text.Rectangle(first.Llx,
                             first.Lly, first.Urx, first.Ury);

            iTextSharp.text.Rectangle secondRect = new iTextSharp.text.Rectangle(second.Llx,
                          second.Lly, second.Urx, second.Ury);

            iTextSharp.text.Rectangle intersectRect = PdfTextBaseRectangle.Intersect(firstRect, secondRect);

            if (intersectRect == null)
                return 0;
          
            float firstRectArea = PdfTextBaseRectangle.GetArea(firstRect);
            float secondRectArea = PdfTextBaseRectangle.GetArea(secondRect);
            float maxArea = (firstRectArea > secondRectArea) ? firstRectArea : secondRectArea;
            float intersectRectArea = PdfTextBaseRectangle.GetArea(intersectRect);

            return intersectRectArea / maxArea;           

        }

        /// <summary>
        /// Devuelve true si los rectángulos comparten
        /// un área superior a la establecidad en las 
        /// opciones de configuración.
        /// </summary>
        /// <param name="first">Rectángulo 1.</param>
        /// <param name="second">Rectángulo 2.</param>
        /// <returns>True si comparten suficiente área
        /// para considerarse como similares.</returns>
        private bool IsAlmostSameArea(PdfTextBaseRectangle first,
            PdfTextBaseRectangle second)
        {
            return (GetCommonAreaCoef(first, second) > 
                Settings.Current.MinRectangleCommon);

        }

        /// <summary>
        /// Devuelve una nueva instancia del catálogo
        /// de jerarquías asociado con el presenta almacén
        /// de patrones.
        /// </summary>
        /// <returns>Catálogo de jerarquías.</returns>
        private IHierarchySet GetHierarchySet()
        {
            Type hierarchySetType = Type.GetType(HierarchySetName);
            return (IHierarchySet)Activator.CreateInstance(hierarchySetType);
        }

        #endregion

    }
}
