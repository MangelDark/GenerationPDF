using GenerationPDF.Interface;
using GenerationPDF.Model;
using GenerationPDF.Methods;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Management;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GenerationPDF.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePDFController : ControllerBase
    {

        private readonly IGeneratePDF _generatePDF;
        private readonly IGenerateCodeBar _generateCodeBar;
        public CreatePDFController(IGeneratePDF generatePDF, IGenerateCodeBar generateCodeBar)
        {
            _generatePDF = generatePDF;
            _generateCodeBar = generateCodeBar;
        }
        // GET: api/<CreatePDFController>
        [HttpGet]
        [Route("ListPrintes")]
        public ActionResult<IEnumerable<PrinterModel>> ListPrintes()
        {

            List<PrinterModel> prList = new List<PrinterModel>();
            foreach (string printerName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                
                var pr = new PrinterModel
                {
                    Name = printerName,
                   
                };
                prList.Add(pr);
            }
           

            //var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");

            //foreach (var printer in printerQuery.Get())
            //{
            //    var pr = new PrinterModel
            //    {
            //        Name = printer.GetPropertyValue("Name").ToString(),
            //        Status = printer.GetPropertyValue("Status").ToString(),
            //        Default = printer.GetPropertyValue("Default").ToString(),
            //        Network = printer.GetPropertyValue("Network").ToString()
            //    };
            //    prList.Add(pr);
            //}
            return Ok(prList);
        }



        // POST api/<CreatePDFController>
        [HttpPost]
        [Route("PrinterDocumentPDF")]
        public ActionResult PrinterDocumentPDF(InformationPDF model)
        {        
               
            var result = _generatePDF.GenerateDocumentPDF(model);
            return Ok(result);
        }
        // POST api/<CreatePDFController>
        [HttpPost]
        [Route("PrinterCodeBar")]
        public ActionResult PrinterCodeBar([FromBody] PrinterCodeBar model)
        {

            var result = _generateCodeBar.GenerarBarCode(model);
            return Ok(result);
        }
      
    }
}
