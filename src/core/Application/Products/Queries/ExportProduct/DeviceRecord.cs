using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Products.Queries.ExportProduct
{
    public class DeviceRecord : IMapFrom<Device>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
