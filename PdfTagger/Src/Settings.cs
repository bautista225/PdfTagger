using PdfTagger.Xml;
using System;
using System.IO;
using System.Xml.Serialization;

namespace PdfTagger
{
    /// <summary>
    /// Contiene la configuración del sistema. Se almacena
    /// en ProgramData\PdfTagger en el archivo settings.xml.
    /// </summary>
    [Serializable]
    [XmlRoot("PdfTagger")]
    public class Settings
    {

        #region Private Member Variables 

        /// <summary>
        /// Parth separator win="\" and linux ="/".
        /// </summary>
        static char _PathSep = System.IO.Path.DirectorySeparatorChar;

        /// <summary>
        /// Configuración actual.
        /// </summary>
        static Settings _Current;

        /// <summary>
        /// Ruta al directorio de configuración.
        /// </summary>
        internal static string Path = System.Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData) + $"{_PathSep}PdfTagger{_PathSep}";


        /// <summary>
        /// Nombre del fichero de configuración.
        /// </summary>
        internal static string FileName = "settings.xml";

        #endregion

        #region Private Methods

        /// <summary>
        /// Inicia estaticos.
        /// </summary>
        /// <returns></returns>
        internal static Settings Get()
        {

            string FullPath = $"{Path}{_PathSep}{FileName}";
          
            if (File.Exists(FullPath))
            {
                _Current = XmlParser.FromXml<Settings>(FullPath);
            }
            else
            {

                _Current = new Settings();

                _Current.InboxPath = Path + $"Inbox{_PathSep}";
                _Current.OutboxPath = Path + $"Outbox{_PathSep}";
                _Current.Protocol = "http";
                _Current.ServerName = "localhost"; // "*";
                _Current.Port = "8099";
                _Current.Root = "Kivu";
                _Current.ActionTag = "op";
                _Current.MachineID = 0;
                _Current.SessionTimeOut = 1000 * 60 * 30;

            }

            CheckDirectories();

            return _Current;
        }

        /// <summary>
        /// Aseguro existencia de directorios de trabajo.
        /// </summary>
        private static void CheckDirectories()
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            if (!Directory.Exists(_Current.InboxPath))
                Directory.CreateDirectory(_Current.InboxPath);

            if (!Directory.Exists(_Current.OutboxPath))
                Directory.CreateDirectory(_Current.OutboxPath);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor estático de la clase Settings.
        /// </summary>
        static Settings()
        {
            Get();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Configuración en curso.
        /// </summary>
        public static Settings Current
        {
            get
            {
                return _Current;
            }
            set
            {
                _Current = value;
            }
        }

        /// <summary>
        /// Ruta al directorio donde se encuentran
        /// los pdf de entrada analizados.
        /// </summary>
        [XmlElement("InboxPath")]
        public string InboxPath { get; set; }

        /// <summary>
        /// Ruta al directorio donde se encuentran
        /// los datos de salida.
        /// </summary>
        [XmlElement("OutboxPath")]
        public string OutboxPath { get; set; }

        /// <summary>
        /// Protocolo.
        /// </summary>
        [XmlElement("Protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// Nombre del servidor.
        /// </summary>
        [XmlElement("ServerName")]
        public string ServerName { get; set; }

        /// <summary>
        /// Directorio raiz del servidor.
        /// </summary>
        [XmlElement("Root")]
        public string Root { get; set; }

        /// <summary>
        /// Tag utilizado para identificar la operaciones o acciones
        /// a ejecutar por el servidor.
        /// </summary>
        [XmlElement("ActionTag")]
        public string ActionTag { get; set; }

        /// <summary>
        /// Puerto del servidor.
        /// </summary>
        [XmlElement("Port")]
        public string Port { get; set; }

        /// <summary>
        /// Identificador de la máquina dentro de la comunidad
        /// de Kivu.
        /// </summary>
        [XmlElement("MachineID")]
        public long MachineID { get; set; }

        /// <summary>
        /// Tiempo máximo de inactividad de sesion
        /// en milisegundos.
        /// </summary>
        [XmlElement("SessionTimeOut")]
        public long SessionTimeOut { get; set; }

        /// <summary>
        /// Tiempo en milisegundos entre revisiones
        /// de la cola de sesiones.
        /// </summary>
        [XmlElement("SessionPoolProcessTime")]
        public int SessionPoolProcessTime { get; set; }

        /// <summary>
        /// Path a la dll que implementa la interfaz
        /// ISqlManager para el almacenamiento de datos.
        /// </summary>
        [XmlElement("SqlManagerPath")]
        public string SqlManagerPath { get; set; }


        #endregion

        #region Public Methods

        /// <summary>
        /// Guarda la configuración en curso actual.
        /// </summary>
        public static void Save()
        {

            CheckDirectories();

            string FullPath = $"{Path}{_PathSep}{FileName}";

            XmlParser.SaveAsXml(Current, FullPath);           

        }

        /// <summary>
        /// Esteblece el archivo de configuración con el cual trabajar.
        /// </summary>
        /// <param name="fileName">Nombre del archivo de configuración a utilizar.</param>
        public static void SetFileName(string fileName)
        {
            FileName = fileName;
            Get();
        }

        #endregion

    }
}
