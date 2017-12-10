using System;
using System.Collections.Generic;
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

        #region Public Properties

        /// <summary>
        /// Colección de patrones.
        /// </summary>
        public List<PdfTagPattern> PdfPatterns { get; set; }

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

    }
}
