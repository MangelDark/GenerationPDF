using GenerationPDF.Model;

namespace GenerationPDF.Interface
{
    public interface IGenerateCodeBar
    {
        HandleMessage GenerarBarCode(PrinterCodeBar model);
        HandleMessage PrintCodeBarPDF(string namePrinter);
    }
}