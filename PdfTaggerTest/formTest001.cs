using iTextSharp.text.pdf.parser;
using PdfTagger;
using PdfTagger.Pdf;
using PdfTagger.Xml;
using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using iTextSharp.text;

namespace PdfTaggerTest
{
    public partial class formTest001 : Form
    {

        Thread _ThreadExtract;

        public formTest001()
        {
            InitializeComponent();
        }

        private void ExtractData(string dirSource, string dirResult, Func<int, int, int>progress)
        {

            string[] pdfPaths = Directory.GetFiles(dirSource, "*.pdf");
            int count = 0;

            foreach (var pdfPath in pdfPaths)
            {
                count++;

                string xmlPath = $"{dirResult}{System.IO.Path.DirectorySeparatorChar}" +
                    $"{System.IO.Path.GetFileNameWithoutExtension(pdfPath)}.xml";

                string rectPath = $"{dirResult}{System.IO.Path.DirectorySeparatorChar}" +
                  $"{System.IO.Path.GetFileNameWithoutExtension(pdfPath)}.pdf";

                string rectPathLines = $"{dirResult}{System.IO.Path.DirectorySeparatorChar}" +
                 $"{System.IO.Path.GetFileNameWithoutExtension(pdfPath)}_lines.pdf";

                progress(count, pdfPaths.Length);

                try
                {
                    // Creamos un PdfUnstructuredDoc para extracción de datos
                    PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(pdfPath);

                    //  Almacenamos los datos extraidos en un archivo xml
                    XmlParser.SaveAsXml(pdf, xmlPath);

                    Util.PrintRectangles(pdfPath, rectPath, pdf, BaseColor.RED);
                    Util.PrintRectangles(pdfPath, rectPathLines, pdf, BaseColor.BLUE, true);
                }
                catch
                {
                }

            }

        }

        private int Progress(int current, int count)
        {
            Invoke(new Action(() => {
                lbProgress.Text = $"Extrayendo datos del archivo pdf {current} de {count}";
                pgbFile.Maximum = count;
                pgbFile.Value = current;
                Refresh();
            }));
            return 0;
        }

        private int ShowError(string msg)
        {
            Invoke(new Action(() => {
                MessageBox.Show(msg);
            }));
            return 0;
        }

        private int SwitchProgress()
        {
            Invoke(new Action(() => {
                pgbFile.Visible = lbProgress.Visible = !pgbFile.Visible; ;
            }));
            return 0;
        }

        private void ExecuteTest()
        {
  

            try
            {
                SwitchProgress();
                ExtractData(txDirSource.Text, txDirResult.Text, Progress);
            }
            catch(Exception ex)
            {
                ShowError($"{ex}");
            }
            finally
            {
                SwitchProgress();
            }

        }

        private void btDirSource_Click(object sender, EventArgs e)
        {
            fdBrw.ShowDialog();
            if (Directory.Exists(fdBrw.SelectedPath))
                txDirSource.Text = fdBrw.SelectedPath;
        }

        private void btDirResult_Click(object sender, EventArgs e)
        {
            fdBrw.ShowDialog();
            if (Directory.Exists(fdBrw.SelectedPath))
                txDirResult.Text = fdBrw.SelectedPath;
        }

        private void btExecute_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists(txDirSource.Text))
            {
                MessageBox.Show("Seleccione un directorio orígen de archivos válido.");
                return;
            }
            if (!Directory.Exists(txDirResult.Text))
            {
                MessageBox.Show("Seleccione un directorio orígen de archivos válido.");
                return;
            }

            _ThreadExtract = new Thread(ExecuteTest);
            _ThreadExtract.Start();
        }
    }
}
