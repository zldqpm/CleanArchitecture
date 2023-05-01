using CsvHelper;
using Application.Common.Interfaces;
using Application.Products.Queries.ExportProduct;
using System.Globalization;

namespace Shared.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildDeviceFile(IEnumerable<DeviceRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
