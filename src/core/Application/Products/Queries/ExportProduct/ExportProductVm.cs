namespace Application.Products.Queries.ExportProduct
{
    public class ExportProductVm
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public byte[]? Content { get; set; }
    }
}
