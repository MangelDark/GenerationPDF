# Print and pdf generator
### It is a Web API which allows you to print to your local printer and generate a pdf file from a React web application.

#### This is the technology and library used for this development:
     * Asp .net core 5 api
     * Itextsharp
     * Jwtbearer
     * RawPrint
     * Swagger ui
   
### RawPrint   
.Net library to send files directly to a Windows printer bypassing the printer driver.
Send PostScript, PCL or other print file types directly to a printer.
Requires .Net 4 runtime on Windows XP to 10 and Server 2003 to 2012.
Usage:

    ```C#
            using RawPrint;

            // Create an instance of the Printer
            IPrinter printer = new Printer();

            // Print the file
            printer.PrintRawFile(PrinterName, Filepath, Filename);
    ```

    

### PDF Generator
You have to call the IGeneratePDF interface and then call the GenerateDocumentPDF method example:
    
    ```C#
        
            private readonly IGeneratePDF _generatePDF;
            public CreatePDFController(IGeneratePDF generatePDF)
            {
                _generatePDF = generatePDF;            
            }

            [HttpPost]
            [Route("PrinterDocumentPDF")]
            public ActionResult PrinterDocumentPDF(InformationPDF model)
            {        

                var result = _generatePDF.GenerateDocumentPDF(model);
                return Ok(result);
            }
     ```   

