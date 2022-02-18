using GenerationPDF.Interface;
using GenerationPDF.Model;
using GenerationPDF.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Management;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GenerationPDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePDFController : ControllerBase
    {

        private readonly IGeneratePDF _generatePDF;
        public CreatePDFController(IGeneratePDF generatePDF)
        {
            _generatePDF = generatePDF;
        }
        // GET: api/<CreatePDFController>
        [HttpGet]
        public ActionResult<IEnumerable<PrinterModel>> Get()
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
        public ActionResult Post([FromBody] InformationPDF model)
        {        
               
            var result = _generatePDF.GenerateDocumentPDF(model);
            return Ok(result);
        }

        // PUT api/<CreatePDFController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CreatePDFController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
