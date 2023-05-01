using AutoMapper;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Dto.Iot
{
    public class DeviceDto : IMapFrom<Device>
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Secret { get; set; }
        public int ProductId { get; set; }
        public int GroupId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Device, DeviceDto>()
              .ForMember(d =>
                d.GroupId, opt =>
                opt.MapFrom(s =>
                  (int)s.GroupId));
        }
    }
}
