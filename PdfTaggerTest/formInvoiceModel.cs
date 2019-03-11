﻿using PdfTagger.Dat;
using PdfTagger.Dat.Met.Bus;
using PdfTagger.Dat.Txt;
using PdfTagger.Pat;
using PdfTagger.Pdf;
using System;
using System.Collections.Generic;

namespace PdfTaggerTest
{

    /// <summary>
    /// Modelo para la vista del formulario formInvoices.
    /// </summary>
    public class formInvoiceModel
    {

        public string PdfPath { get; private set; }

        public PdfUnstructuredDoc Pdf { get; private set; }

        public InvoiceMetadata Invoice { get; set; }

        public PdfTagPatternStore Store { get; private set; }

        public PdfTagExtractionResult ExtractionResult { get; private set; }

        public List<PdfTagPattern> TextStringInfosFiltered { get; private set; }

        public List<PdfTagPattern> WordGroupsFiltered { get; private set; }

        public List<PdfTagPattern> PdfTextInfosFiltered { get; private set; }

        /// <summary>
        /// Carga un documento pdf.
        /// </summary>
        /// <param name="path">Documento pdfs a analizar.</param>
        public void LoadPdfInvoiceDoc(string path)
        {
            PdfPath = path;
            Pdf = new PdfUnstructuredDoc(PdfPath);
            Pdf.DocCategory = "Invoice";
        }

        /// <summary>
        /// Carga del alamcén en su caso los grupos de palabras.
        /// </summary>
        /// <param name="name">Nombre del MetaDataItem a recuperar.</param>
        /// <param name="source">Nombre del Pattern source a recuperar.</param>
        public void LoadWordGroupFromStore(string name, string source)
        {
            if (Store == null)
                return;

            List<PdfTagPattern> target = null;

            if (source == "WordGroupsInfos")
                target = WordGroupsFiltered = new List<PdfTagPattern>();
            else if (source == "PdfTextInfos")
                target = PdfTextInfosFiltered = new List<PdfTagPattern>();

            foreach (var patt in Store.PdfPatterns)
                if (patt.SourceTypeName == source 
                    && patt.MetadataItemName == name)
                    target.Add(patt);

        }

        public void LoadTextStringFromStore(string name, string source = "TextStringInfos")
        {
            if (Store == null)
                return;

            List<PdfTagPattern> target = null;

            if (source == "TextStringInfos")
                target = TextStringInfosFiltered = new List<PdfTagPattern>();

            foreach (var patt in Store.PdfPatterns)
                if (patt.SourceTypeName == source &&
                    patt.MetadataItemName == name)
                    target.Add(patt);
        }

        /// <summary>
        /// Ejecuta el aprendizaje de patrones sobre unos
        /// metadatos de factura y datos leídos de pdf previos.
        /// </summary>
        public void ExecutePatternsLearning()
        {
            if (string.IsNullOrEmpty(Pdf.DocID))
                throw new InvalidOperationException("Es necesario un valor Pdf.DocID.");

            PdfCompareResult compareResult = PdfCompare.Compare(new BusinessInvoiceHierarchySet(), Pdf, Invoice);
            PdfTagPatternFactory.Save(compareResult);
        }

        /// <summary>
        /// Intenta la extracción de datos mediante patrones
        /// aprendidos.
        /// </summary>
        public void Extract()
        {

            try
            {
                Store = PdfTagPatternFactory.GetStore(Pdf);
            }
            catch
            {
            }

            if (Store != null)
            {
                ExtractionResult = Store.Extract(Pdf);
                Invoice = ExtractionResult.Metadata as InvoiceMetadata;
            }
        }

    
    }
}
