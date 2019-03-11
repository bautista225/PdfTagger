using org.pdfclown.documents;
using org.pdfclown.documents.contents;
using org.pdfclown.documents.contents.objects;
using org.pdfclown.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTagger.Pdf
{

    public class PdfClownTextExtractor
    {
        #region Private Properties

        /// <summary>
        /// Extractor de texto de PDFs de PdfClown
        /// </summary>
        private TextExtractor extractor;

        /// <summary>
        /// Almacena todos los textStrings recogidos en el método TextInfoExtraction
        /// </summary>
        private List<PdfClownTextString> _PdfTextStrings;

        #endregion

        #region Private Methods

        /// <summary>
        /// Extraccción del texto de la página pasada por parámetro
        /// con sus respectivas propiedades
        /// (font, font size, text color, text rendering mode, text bounding box, etc.).
        /// Este escaneo se realiza por niveles, ya que las página están representadas 
        /// por una secuencia de Content Objects, posiblemente anidados en múltiples niveles.
        /// </summary>
        /// <param name="level">Nivel que estamos iterando</param>
        private void Extract(ContentScanner level)
        {
            if (level == null)
                return;

            while (level.MoveNext())
            {
                ContentObject content = level.Current;

                if (content is Text)
                {
                    //Guardamos los TextStrings con sus distintas propiedades
                    ContentScanner.TextWrapper text = (ContentScanner.TextWrapper)level.CurrentWrapper;

                    foreach (ContentScanner.TextStringWrapper textString in text.TextStrings)
                    {
                        _PdfTextStrings.Add(new PdfClownTextString(
                            textString.Text, 
                            textString.Style.FillColor, 
                            textString.Style.StrokeColor,
                            textString.Style.Font,
                            textString.Style.FontSize));
                    }
                }
                else if (content is XObject)
                {
                    //Scanning the external level
                    try
                    {
                        Extract(((XObject)content).GetScanner(level));
                    }
                    catch { }
                }
                else if (content is ContainerObject)
                {
                    //Scanning the inner level
                    try
                    {
                        Extract(level.ChildLevel);
                    }
                    catch { }
                }
                
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creación de un nuevo PdfClown TextXtractor
        /// </summary>
        public PdfClownTextExtractor()
        {
            extractor = new TextExtractor();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Prompts the user for advancing to the next page.
        /// </summary>
        /// <param name="page">Next page.</param>
        /// <param name="skip">Whether the prompt has to be skipped.</param>
        /// <returns></returns>
        protected bool PromptNextPage(Page page, bool skip)
        {
            int pageIndex = page.Index;

            if (pageIndex > 0 && !skip)
            {
                IDictionary<string, string> options = new Dictionary<string, string>();

                options[""] = "Scan next page";
                options["Q"] = "End scanning";

                if (!PromptChoice(options).Equals(""))
                    return false;
            }
            Console.WriteLine("\nScanning page " + (pageIndex + 1) + "...\n");

            return true;
        }

        /// <summary>
        /// Gets the user's choice from the given options.
        /// </summary>
        /// <param name="options">Available options to show to the user.</param>
        /// <returns>Chosen option key.</returns>
        protected string PromptChoice(IDictionary<string, string> options)
        {
            Console.WriteLine();

            foreach (KeyValuePair<string, string> option in options)
            {

                Console.WriteLine(
                    (option.Key.Equals("") ? "ENTER" : "[" + option.Key + "]")
                    + " " + option.Value
                    );
            }
            Console.Write("Please select: ");

            return Console.ReadLine();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Extraccción del texto de la página pasada por parámetro
        /// con sus respectivas propiedades
        /// (font, font size, text color, text rendering mode, text bounding box, etc.).
        /// </summary>
        /// <param name="page">Página de donde extraer el texto</param>
        public void TextInfoExtraction(Page page)
        {
            _PdfTextStrings= new List<PdfClownTextString>();
            ContentScanner level = new ContentScanner(page);
            Extract(level);
        }

        /// <summary>
        /// Devuelve la lista de TextStrings extraida
        /// </summary>
        /// <returns>Lista de TextStrings</returns>
        public List<PdfClownTextString> GetTextStrings()
        {
            return _PdfTextStrings;
        }

        #endregion
    }
}
