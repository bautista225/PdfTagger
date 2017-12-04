using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfTagger.Pdf;
using System.Collections.Generic;
using System.IO;

namespace PdfTaggerTest
{
    public class Util
    {

        /// <summary>
        /// Dibuja los rectángulos configurados en un pdf.
        /// </summary>
        /// <param name="pathTarget">Pdf destino.</param>
        /// <param name="baseColor">Color de los rectangulos.</param>
        public static void PrintRectangles(string pathSource, string pathTarget, 
            PdfUnstructuredDoc pdf, BaseColor baseColor, bool lines = false)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(pathSource);

                PdfStamper pdfStamper = new PdfStamper(pdfReader,
                      new FileStream(pathTarget, FileMode.OpenOrCreate));


                int p = 0;

                foreach (var page in pdf.PdfUnstructuredPages)
                {
                    p++;
                    PdfContentByte cb = pdfStamper.GetOverContent(p);

                    List<PdfTextRectangle> rectangles = (lines) ? page.Lines : page.WordGroups;

                    foreach (var reg in rectangles)
                    {
                        cb.SetColorStroke(baseColor);

                        iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(
                            reg.Llx, reg.Lly, reg.Urx, reg.Ury);

                        cb.Rectangle(rect.Left, rect.Bottom,
                            rect.Width, rect.Height);
                        cb.Stroke();
                    }

                }
                pdfStamper.Close();

            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (DocumentException ex)
            {
                throw ex;
            }

        }
    }
}
