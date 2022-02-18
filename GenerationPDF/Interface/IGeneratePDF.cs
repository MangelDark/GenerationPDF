using GenerationPDF.Model;

namespace GenerationPDF.Interface
{
    public interface IGeneratePDF
    {
        string GenerateDocumentPDF(InformationPDF model);
    }
}