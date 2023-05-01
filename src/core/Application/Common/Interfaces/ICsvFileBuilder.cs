using Application.Products.Queries.ExportProduct;

namespace Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildDeviceFile(IEnumerable<DeviceRecord> records);
    }
}
