Imports PdfTagger.Pdf
Imports PdfTagger.Xml

Public Class formTest001
    Private Sub formTest001_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Creamos un PdfUnstructuredDoc para extracción de datos
        Dim pdf As PdfUnstructuredDoc = New PdfUnstructuredDoc("C:\test.pdf")

        ' Almacenamos los datos extraidos en un archivo xml
        XmlParser.SaveAsXml(pdf, "C:\test.xml")
    End Sub
End Class
