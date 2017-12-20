using iTextSharp.text;
using PdfTagger.Dat.Met.Bus;
using PdfTagger.Pat;
using PdfTagger.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTaggerTest
{
    public partial class formInvoices : Form
    {

        formInvoiceModel _Model;

        public formInvoices()
        {
            InitializeComponent();
            _Model = new formInvoiceModel();            
        }

        private void mnMainOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        /// <summary>
        /// Abre el archivo pdf.
        /// </summary>
        private void Open()
        {
            dlgOpen.ShowDialog();
            if (File.Exists(dlgOpen.FileName))
            {
                ClearView();
                _Model.LoadPdfInvoiceDoc(dlgOpen.FileName);
                ShowInBrowser(_Model.PdfPath);
            }
        }

        /// <summary>
        /// Carga 
        /// </summary>
        /// <param name="pdfPath">Ruta pdf.</param>
        private void ShowInBrowser(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
            {
                wBr.Visible = false;         
            }
            else
            {
                wBr.Visible = true;
                wBr.Navigate(pdfPath);
            }
        }

        private InvoiceMetadata GetMetadataFromView()
        {

            InvoiceMetadata invoice = new InvoiceMetadata();

            NumberFormatInfo numFormatInfo = new NumberFormatInfo();
            numFormatInfo.NumberDecimalSeparator = ".";


            foreach (PropertyInfo pInf in invoice.GetType().GetProperties())
            {

                Control[] ctlrs = Controls.Find($"tx{pInf.Name}", true);

                if (ctlrs.Length != 0)
                {
                    TextBox txb = (TextBox)ctlrs[0];

                    if (!string.IsNullOrEmpty(txb.Text))
                    {

                        object pValue = null;

                        if (txb.TextAlign == HorizontalAlignment.Right)
                            pValue = System.Convert.ToDecimal(txb.Text, numFormatInfo);
                        else
                            pValue = Convert(pInf.PropertyType, txb.Text);

                        pInf.SetValue(invoice, pValue);
                    }
                }
                
            }

            return invoice;
        }

        private void GetViewFromMetadata()
        {

            if (_Model.Invoice == null)
                return;

            InvoiceMetadata invoice = _Model.Invoice;

            NumberFormatInfo numFormatInfo = new NumberFormatInfo();
            numFormatInfo.NumberDecimalSeparator = ".";


            foreach (PropertyInfo pInf in invoice.GetType().GetProperties())
            {

                Control[] ctlrs = Controls.Find($"tx{pInf.Name}", true);

                if (ctlrs.Length != 0 && pInf.Name != "SellerPartyID")
                {
                    TextBox txb = (TextBox)ctlrs[0];

                    object pValue = pInf.GetValue(invoice);

                    if (pValue != null)
                    {
                        if (pInf.PropertyType == typeof(decimal) &&
                            System.Convert.ToDecimal(pValue) != 0)
                            txb.Text = ((decimal)pValue).ToString(numFormatInfo);
                        else if (pInf.PropertyType == typeof(DateTime?) && pValue != null)
                            txb.Text = ((DateTime)pValue).ToString("dd/MM/yyyy");
                        else
                            txb.Text = $"{pValue}";
                    }
                      
                    
                }

            }

        }

        private void ClearView()
        {

            foreach (PropertyInfo pInf in typeof(InvoiceMetadata).GetProperties())
            {
                Control[] ctlrs = Controls.Find($"tx{pInf.Name}", true);

                if (ctlrs.Length != 0)
                    ((TextBox)ctlrs[0]).Text="";
            }
           
        }

        private object Convert(Type type, string text)
        {
            try
            {

                var converter = TypeDescriptor.GetConverter(type);

                if (converter != null)
                    return converter.ConvertFromString(text);

                return Activator.CreateInstance(type); 
            }
            catch (NotSupportedException)
            {
                return Activator.CreateInstance(type);
            }
        }

        private void LoadPreView(bool lines = false)
        {

            if(string.IsNullOrEmpty(_Model.PdfPath))
            {
                MessageBox.Show("Debe abrir un pdf.");
                return;
            }

            if (_Model.Pdf==null)
            {
                MessageBox.Show("Debe abrir un pdf.");
                return;
            }

            string fileSufix = (lines) ? "_ln" : "_wg";

            string fileName = $"{Path.GetDirectoryName(_Model.PdfPath)}{Path.DirectorySeparatorChar}";
            fileName += $"{Path.GetFileNameWithoutExtension(_Model.PdfPath)}{fileSufix}";
            fileName += $"{Path.GetExtension(_Model.PdfPath)}";

            BaseColor color = (lines) ? BaseColor.BLUE : BaseColor.RED; 

            Util.PrintRectangles(_Model.PdfPath, fileName, _Model.Pdf, color, lines);

            wBr.Navigate(fileName);
        }

        private void formInvoices_Shown(object sender, EventArgs e)
        {
            sPnH.SplitterDistance = Height - mnMain.Height - 
                tbMain.Height - stMain.Height - 90;
        }

        private void txSellerPartyID_Validated(object sender, EventArgs e)
        {
            if (_Model.Pdf != null && !String.IsNullOrEmpty(txSellerPartyID.Text))
                new Thread(Render).Start();
        }

        private void Render()
        {
            if (_Model.Pdf != null && !String.IsNullOrEmpty(txSellerPartyID.Text))
            {
                _Model.Pdf.DocID = txSellerPartyID.Text;
                _Model.Extract();

                Invoke(new Action(()=> {
                    GetViewFromMetadata();
                    LoadPatternStore();
                }));                
            }
        }

        private void LoadPatternStore()
        {

            grdPatternStore.Rows.Clear();

            if (_Model.Store == null)
                return;

            tbPatternStore.Text = $"PatternStore({ _Model.Store.PdfPatterns.Count})";

            _Model.Store.PdfPatterns.Sort();

            foreach (var patt in _Model.Store.PdfPatterns)
            {
                int index = grdPatternStore.Rows.Add(patt.MetadataItemName,
                  patt.PdfPageN, patt.PdfRectangle, patt.MatchesCount,
                  patt.RegexPattern, patt, "", patt.IsLastPage, 
                  patt.SourceTypeName, patt.Position);
            }
        }

        private void ExecuteLearning()
        {            
            _Model.Invoice = GetMetadataFromView();

            if (string.IsNullOrEmpty(_Model.Invoice.InvoiceNumber))
            {
                MessageBox.Show("InvoiceNumber no válido.");
                return;
            }

            if (string.IsNullOrEmpty(_Model.Invoice.BuyerPartyID))
            {
                MessageBox.Show("BuyerPartyID no válido.");
                return;
            }

            if (_Model.Invoice.IssueDate == null)
            {
                MessageBox.Show("IssueDate no válido.");
                return;
            }

            if(_Model.Invoice.GrossAmount == 0)
            {
                MessageBox.Show("GrossAmount no válido.");
                return;
            }

            decimal cuadre = _Model.Invoice.GrossAmount - _Model.Invoice.TaxesOutputsBase01 -
                _Model.Invoice.TaxesOutputsBase02 - _Model.Invoice.TaxesOutputsBase03 -
                _Model.Invoice.TaxesOutputsBase04 - _Model.Invoice.TaxesOutputsAmount01 -
                _Model.Invoice.TaxesOutputsAmount02 - _Model.Invoice.TaxesOutputsAmount03 -
                _Model.Invoice.TaxesOutputsAmount04 + _Model.Invoice.TaxesWithholdingAmount01;

            if (cuadre != 0)
            {
                MessageBox.Show("Cuadre importes no válido.");
                return;
            }

            _Model.ExecutePatternsLearning();
        }

        private void mnMainAnalyze_Click(object sender, EventArgs e)
        {
            ExecuteLearning();
        }

        private void mnMainViewWordGroups_Click(object sender, EventArgs e)
        {
            LoadPreView();
        }

        private void mnMainViewLines_Click(object sender, EventArgs e)
        {
            LoadPreView(true);
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox next = GetNextText(sender as TextBox);
                if (next != null)
                {
                    next.Focus();
                    next.SelectAll();
                }
            }

            if (e.KeyCode == Keys.Escape)
                (sender as TextBox).Text = "";

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
                e.Handled = e.SuppressKeyPress = true;

        }

        private TextBox GetNextText(TextBox txb)
        {
            foreach (var ctrl in tbInvoice.Controls)
            {
                TextBox nxt = ctrl as TextBox;

                if (nxt != null && nxt.TabIndex == txb.TabIndex + 1)
                    return nxt;
            }

            return null;

        }

        private void ctxMnTxViewPatterns_Click(object sender, EventArgs e)
        {

            if(_Model.Store==null)
            {
                MessageBox.Show("Almacén de patrones no seleccionado.");
                return;
            }

            ToolStripMenuItem mnItem = (sender as ToolStripMenuItem);
            ContextMenuStrip ctx = (ContextMenuStrip)mnItem.GetCurrentParent();
            TextBox txb = (TextBox)ctx.SourceControl;

            string itemName = txb.Name;

            itemName = itemName.Substring(2, itemName.Length - 2);

            var patterns = _Model.Store.GetPdfPatterns("WordGroupsInfos", itemName);

            string fileSufix = $"_{itemName}_wg";

            string fileName = $"{Path.GetDirectoryName(_Model.PdfPath)}{Path.DirectorySeparatorChar}";
            fileName += $"{Path.GetFileNameWithoutExtension(_Model.PdfPath)}{fileSufix}";
            fileName += $"{Path.GetExtension(_Model.PdfPath)}";
            

            Util.PrintRectangles(_Model.PdfPath, fileName, patterns);

            wBr.Navigate(fileName);

        }

        private void LoadResultPatterns(object sender, EventArgs e)
        {

            TextBox txb = (sender as TextBox);

            string metaDataItemName = txb.Name.Substring(2, txb.Name.Length-2);

            _Model.LoadWordGroupFromStore(metaDataItemName, "WordGroupsInfos");
            _Model.LoadWordGroupFromStore(metaDataItemName, "PdfTextInfos");

            FillGrid(grdWordGroups, _Model.WordGroupsFiltered);
            FillGrid(grdPdfText, _Model.PdfTextInfosFiltered);

            if(_Model.WordGroupsFiltered!=null)
                tbWordGroups.Text = $"WordGroups {metaDataItemName}({_Model.WordGroupsFiltered.Count})";

            if (_Model.PdfTextInfosFiltered != null)
                tbPdfText.Text = $"PdfText {metaDataItemName}({_Model.PdfTextInfosFiltered.Count})";

            Refresh();

        }

        private void FillGrid(DataGridView grd, List<PdfTagPattern> patterns)
        {
            grd.Rows.Clear();

            if (patterns == null)
                return;

            foreach (var patt in patterns)
            {
                int index = grd.Rows.Add(patt.MetadataItemName,
                    patt.PdfPageN, patt.PdfRectangle, patt.MatchesCount,
                    patt.RegexPattern, patt);


                if (IsInResults(patt, out object value))
                {
                    grd.Rows[index].DefaultCellStyle.BackColor = Color.Green;
                    grd.Rows[index].Cells[6].Value = value;
                }
            }
        }

        private bool IsInResults(PdfTagPattern patt, out object value)
        {

            value = null;

            if (_Model.ExtractionResult == null)
                return false;

            if (_Model.ExtractionResult.Results.ContainsKey(patt.MetadataItemName))
            {
                foreach (var pattResult in _Model.ExtractionResult.Results[patt.MetadataItemName])
                {
                    if (pattResult.Pattern.Equals(patt))
                    {
                        value = pattResult.Value;
                        return true;
                    }
                }
            }

            return false;

        }
   
    }
}
