using GenerationPDF.Interface;
using GenerationPDF.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using RawPrint;
using RawPrintGeneric.Class;
using System;
using System.Globalization;
using System.IO;

namespace GenerationPDF.Methods
{
    public class GeneratePDF : IGeneratePDF
    {

        public GeneratePDF()
        {

        }


        public string GenerateDocumentPDF(InformationPDF model)

        {
            try
            {
                var NowUTC = DateTime.Now.ToString("dd/MM/yyyy").Replace("/","-");
                
                var NameFullDocument = "TemplateDocument-"+NowUTC;
             
                Document doc = new Document();
                using (var fs = new FileStream($@"{NameFullDocument}.pdf", FileMode.Create))
                {
                    using (var write = PdfWriter.GetInstance(doc, fs))
                    {
                        doc.Open();
                        Paragraph title = new Paragraph();
                        title.Font = FontFactory.GetFont(FontFactory.TIMES, 18f, BaseColor.BLUE);
                        title.Add(model.Title);
                        doc.Add(title);
                        doc.Add(new Paragraph(model.Subject));
                        doc.Add(new Paragraph(model.Body));
                                   
                        doc.Close();
                        write.Close();
                        IPrinter printer = new RawPrint.Printer();
                        printer.OnJobCreated += (sender, eventArgs) => { Console.WriteLine("Job started."); };
                        printer.PrintRawFile(model.NamePrinter, fs.Name, NameFullDocument);
                    }
                    FileInfo file = new FileInfo(fs.Name);

                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                        Console.Write("file deleted successfully");
                    }
                }

                
                return "Archivo fue impreso correctamente.";

            }
            catch (Exception ex)
            {

                return ex.Message;
            }



        }
    }
}
