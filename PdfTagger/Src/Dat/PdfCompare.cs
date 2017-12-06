﻿/*
    This file is part of the PdfTagger (R) project.
    Copyright (c) 2017-2018 Irene Solutions SL
    Authors: Irene Solutions SL.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    IRENE SOLUTIONS SL. IRENE SOLUTIONS SL DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    http://pdftagger.com/terms-of-use/
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the PdfTagger software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving extract PDFs data on the fly in a web application, shipping PdfTagger
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */
using PdfTagger.Dat.Txt;
using PdfTagger.Pdf;
using System.Reflection;

namespace PdfTagger.Dat
{

    /// <summary>
    /// Representa una comparación entre los datos 
    /// obtenidos de un pdf y un conjunto de metadatos.
    /// </summary>
    public class PdfCompare
    {

        #region Private Methods

        /// <summary>
        /// Devuelve true si el objeto es
        /// numérico y cero.
        /// </summary>
        /// <param name="value">Valor a chequear.</param>
        /// <returns>True si cero numérico y false si no.</returns>
        private static bool IsZeroNumeric(object value)
        {
            if (!float.TryParse($"{value}", out float result))
                return false;

            return result == 0;

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Devuelve las coincidencias entre los datos
        /// del pdf, y los metadatos facilitados.
        /// <code lang="C#">
        ///         // Partiendo de una entrada de datos no estructurados de pdf 
        ///         PdfUnstructuredDoc pdf = new PdfUnstructuredDoc(@"C:\ProgramData\PdfTagger\Inbox\0000021101.pdf");
        /// 
        ///         // y de un conjunto de datos estructurados
        ///         InvoiceMetadata metadata = new InvoiceMetadata();
        /// 
        ///         metadata.InvoiceNumber = "1 / 33050";
        ///         metadata.BuyerPartyID = "ES - A12070330";
        ///         metadata.IssueDate = new DateTime(2017, 11, 30);
        ///         metadata.GrossAmount = 3646.50m;
        ///         metadata.TaxesOutputsBase01 = 3013.64m;
        ///         metadata.TaxesOutputsRate01 = 21m;
        ///         metadata.TaxesOutputsAmount01 = 632.86m;
        /// 
        ///         PdfCompareResult compareResult = PdfCompare.Compare(new BusinessHierarchySet(), pdf, metadata);
        /// </code> 
        /// <code lang="VB">
        ///       ' Partiendo de una entrada de datos no estructurados de pdf 
        ///        Dim pdf As PdfUnstructuredDoc = New PdfUnstructuredDoc(@"C:\ProgramData\PdfTagger\Inbox\0000021101.pdf")
        ///
        ///        ' y de un conjunto de datos estructurados
        ///        Dim metadata As InvoiceMetadata = New InvoiceMetadata()
        ///
        ///        metadata.InvoiceNumber = "1 / 33050"
        ///        metadata.BuyerPartyID = "ES - A12070330"
        ///        metadata.IssueDate = New Date(2017, 11, 30)
        ///        metadata.GrossAmount = CDec(3646.5)
        ///        metadata.TaxesOutputsBase01 = CDec(3013.64)
        ///        metadata.TaxesOutputsRate01 = 21
        ///        metadata.TaxesOutputsAmount01 = CDec(632.86)
        ///
        ///        Dim compareResult As PdfCompareResult = PdfCompare.Compare(New BusinessHierarchySet(), pdf, metadata)
        /// </code>  
        /// </summary>
        /// <param name="businessHierarchySet">Catalogo de jerarquías de analizadores
        /// por tipo. La operación utilizara para comparar cada tipo de variable
        /// el parser obtenido del catálogo. La comparación se irá ejecutando
        /// por cada uno de los parsers según su orden en la jerarquía, hasta
        /// que se encuentre un valor coincidente o se llegue al final
        /// de la jerarquía.</param>
        /// <param name="pdf">Instancia de la clase PdfUnstructuredDoc fruto
        /// del análisis y obtención de los datos no estructurados de un pdf.</param>
        /// <param name="metadata">Datos estructurados a comparar con los
        /// datos no estructurados obtenidos del pdf.</param>
        /// <returns>Instancia de la clase PdfCompareResult con
        /// los resultados obtenidos de la comparación.</returns>
        public static PdfCompareResult Compare(BusinessHierarchySet businessHierarchySet, 
            PdfUnstructuredDoc pdf, IMetadata metadata)
        {
            PdfCompareResult compareResult = new PdfCompareResult();

            foreach (PropertyInfo pInf in metadata.GetType().GetProperties())
            {
                object pValue = pInf.GetValue(metadata);

                // Obtengo la jerarquía de analizadores
                ITextParserHierarchy parserHierarchy = businessHierarchySet.GetParserHierarchy(pInf.PropertyType);
                
                // Recorro todos los datos del pdf que quiero comparar
                if (parserHierarchy != null && pValue != null && !IsZeroNumeric(pValue))
                {
                    foreach (var page in pdf.PdfUnstructuredPages)
                    {
                        foreach (var wordGroup in page.WordGroups)
                            foreach (var match in parserHierarchy.GetMatches(pValue,
                                wordGroup.Text))
                                compareResult.WordGroupsInfos.Add(new PdfCompareInfo(pdf, page, wordGroup, match, pInf));

                        foreach (var line in page.Lines)
                            foreach (var match in parserHierarchy.GetMatches(pValue,
                                line.Text))
                                compareResult.LinesInfos.Add(new PdfCompareInfo(pdf, page, line, match, pInf));

                    }
                }
            }

            return compareResult;

        }

        #endregion

    }
}

