using PdfTagger.Dat;
using PdfTagger.Xml;
using System.IO;

namespace PdfTagger.Pat
{

    /// <summary>
    /// Operaciones con patrones.
    /// </summary>
    public class PdfTagPatternFactory
    {

        #region Private Methods

        /// <summary>
        /// Devuelve el directorio de almacenamiento de patrones.
        /// </summary>
        /// <param name="docCategory">Categoría de documento.</param>
        /// <returns>Ruta del directorio de almacenamiento de patrones.</returns>
        private static string GetDirectory(string docCategory)
        {
            string dir = $"{Settings.Current.PatternsPath}{docCategory}" +
                $"{Path.DirectorySeparatorChar}";

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return dir;
        }

        /// <summary>
        /// Devuelve un nombre de archivo para almacenar
        /// los patrones de un documento determinado.
        /// </summary>
        /// <param name="docID">Id. del documento.</param>
        /// <returns>Nombre del archivo de almacenamiento
        /// de patrones.</returns>
        private static string GetFileName(string docID)
        {
            return $"{docID}.xml";
        }

        /// <summary>
        /// Devuelve el nombre de archivo incluyendo la ruta
        /// donde se almacenan los patrones para un documento
        /// determinado.
        /// </summary>
        /// <param name="compareResult">Resuiltado de comparación.</param>
        /// <returns>Ruta completa al archivo.</returns>
        private static string GetPath(PdfCompareResult compareResult)
        {
           return $"{GetDirectory(compareResult.DocCategory)}"+
                $"{GetFileName(compareResult.DocID)}";
        }

        /// <summary>
        /// Crea un nuevo almacén de patrones.
        /// </summary>
        /// <param name="compareResult">Resultado de una comparación.</param>
        /// <param name="path">Ruta del store.</param>
        private static void Create(PdfCompareResult compareResult, string path)
        {

            PdfTagPatternStore store = new PdfTagPatternStore();

            foreach (var info in compareResult.WordGroupsInfos)
            {
                PdfTagPattern pattern = info.GetPdfTagPattern();
                pattern.SourceTypeName = "WordGroupsInfos";
                FillPdfTagPattern(compareResult, pattern);
                store.PdfPatterns.Add(pattern);
            }

            foreach (var info in compareResult.LinesInfos)
            {
                PdfTagPattern pattern = info.GetPdfTagPattern();
                pattern.SourceTypeName = "LinesInfos";
                FillPdfTagPattern(compareResult, pattern);
                store.PdfPatterns.Add(pattern);
            }

            foreach (var info in compareResult.PdfTextInfos)
            {
                PdfTagPattern pattern = info.GetPdfTagPattern();
                pattern.SourceTypeName = "PdfTextInfos";
                FillPdfTagPattern(compareResult, pattern);
                store.PdfPatterns.Add(pattern);
            }

            XmlParser.SaveAsXml(store, path);

        }

        /// <summary>
        /// Devuelve un patrón de búsqueda a partir de la 
        /// instancia de coicidencia.
        /// </summary>
        /// <returns>Patrón de búsqueda.</returns>
        private static void FillPdfTagPattern(PdfCompareResult compareResult, PdfTagPattern pattern)
        {
            pattern.DocCategory = compareResult.DocCategory;
            pattern.DocID = compareResult.DocID;
            pattern.HierarchySetName = compareResult.HierarchySetName;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Guarda patrones obtenidos de una oparación de comparación
        /// determinada.
        /// </summary>
        /// <param name="compareResult">Resultado de una comparación.</param>
        public static void Save(PdfCompareResult compareResult)
        {

            // Si el archivo de patrones no existe creo un nuevo

            string file = GetPath(compareResult);

            if (!File.Exists(file))
                Create(compareResult, file);

            // Si el archivo de patrones existe, lo actualizo con los nuevos
            // resultados (inserto nuevos, elimina sobrantes poco efectivos, y
            // actualizo matchCount.

        }

        #endregion

   

    }
}
