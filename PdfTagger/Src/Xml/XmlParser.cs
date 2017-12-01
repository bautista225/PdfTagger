using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PdfTagger.Xml
{

    /// <summary>
    /// Encargado de la serialización y deserialización
    /// de archivos xml.
    /// </summary>
    public class XmlParser
    {

        /// <summary>
        /// Serializa el objeto como xml y lo guarda
        /// en la ruta indicada.
        /// </summary>
        /// <param name="instance">Instancia de objeto a serializar.</param>
        /// <param name="path">Ruta al archivo xml a crear.</param>
        public static void SaveAsXml(object instance, string path)
        {
            XmlSerializer serializer = new XmlSerializer(instance.GetType());

            using (StreamWriter w = new StreamWriter(path))
            {
                serializer.Serialize(w, instance);
            }
        }

        /// <summary>
        /// Obtiene una instancia de objeto a partir de un
        /// archivo xml.
        /// </summary>
        /// <param name="path">Ruta al archivo.</param>
        /// <returns>Instancia de tipo T.</returns>
        public static T FromXml<T>(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    return (T)serializer.Deserialize(r);
                }
            }
            else
            {
                throw new FileNotFoundException($"No se encontró el archivo: {path}");
            }
        }
    }
}
