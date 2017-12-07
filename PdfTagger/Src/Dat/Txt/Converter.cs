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
    http://pdftagger.com/terms-of-use.pdf/
    
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
using System;
using System.ComponentModel;

namespace PdfTagger.Dat.Txt
{

    /// <summary>
    /// Conversor de texto al tipo T.
    /// </summary>
    /// <typeparam name="T">Tipo T al que se
    /// convierte el texto.</typeparam>
    public class Converter<T> : IConverter<T>
    {

        #region Public Methods

        /// <summary>
        /// Valor del tipo T obtenido mediante
        /// el conversor.
        /// </summary>
        /// <param name="text">Texto a convertir.</param>
        /// <returns>Valor T representado por el texto.</returns>
        public virtual T Convert(string text)
        {
            try
            {

                var converter = TypeDescriptor.GetConverter(typeof(T));

                if (converter != null)
                    return (T)converter.ConvertFromString(text);

                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }

        #endregion

    }
}
