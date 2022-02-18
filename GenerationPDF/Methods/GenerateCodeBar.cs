using GenerationPDF.Interface;
using GenerationPDF.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;

namespace GenerationPDF.Methods
{
    public class GenerateCodeBar : IGenerateCodeBar
    {
        public HandleMessage GenerarBarCode(PrinterCodeBar model)
        {
            try
            {
                using var barcodeAPI = new BarcodeLib.Barcode();


                int imageWidth = 190;
                int imageHeight = 60;
                Color foreColor = Color.Black;
                Color backColor = Color.Transparent;
                string data = model.CodeBar;
                //Habilitamos el lable para el codigo de barrar
                barcodeAPI.IncludeLabel = true;
                //Insertamos el codigo 
                barcodeAPI.AlternateLabel = data;
                //Le indicamos la posicion en la que se va mostrar el codigo
                barcodeAPI.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;

                System.Drawing.Image img = barcodeAPI.Encode(BarcodeLib.TYPE.CODE39, data, Color.Black, Color.White, imageWidth, imageHeight);
                // Almacene la imagen en alguna ruta con el formato deseado
                // 
                img.Save(@"CodeBar.png", ImageFormat.Png);
                img.Dispose();
                //AppDomain root = AppDomain.CurrentDomain;     
                //string result = @"TempFile\CodeBar.png";
                var result =   PrintCodeBarPDF(model.NamePrinter);
                return result;
            }
            catch (Exception ex)
            {

                HandleMessage message;
                message = new HandleMessage()
                {
                    Message = ex.Message,
                    Error = ex.StackTrace,
                    Status = ex.HResult,
                    TypeMessage = "El metodo que genera el código de barrar"
                };
                return message;
            }


        }

        public HandleMessage  PrintCodeBarPDF(string namePrinter)
        {
            try
            {


                HandleMessage message;
                using var printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                printDocument.PrinterSettings.PrinterName = namePrinter;
                printDocument.PrinterSettings.Copies = 1;
                printDocument.Print();
                printDocument.Dispose();

                FileInfo file = new FileInfo(@"CodeBar.png");
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();

                    message = new HandleMessage() 
                    {
                        Message = "Archivo eliminado correctamente.",
                        Error = "Ningú Error Encontrado.",
                        Status = 200,
                        TypeMessage = "Metodo de impresión de código de barra."
                    };
                    return message;
                }
                else
                {
                   
                    return null;
                }         
                
            }
            catch (Exception ex)
            {
                HandleMessage message;
                message = new HandleMessage()
                {
                    Message = ex.Message,
                    Error = ex.StackTrace,
                    Status = ex.HResult,
                    TypeMessage = "El metodo para imprimir el código de barrar"
                };

                return message;
            }
        
           
        }

        void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {

#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            using (var newImage = Image.FromFile(@"CodeBar.png"))
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
            {
                Rectangle rectangle = new Rectangle(-5, 20, 50, 50);
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
                ev.Graphics.DrawImageUnscaled(newImage, rectangle);
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
            }



        }


    }
}
