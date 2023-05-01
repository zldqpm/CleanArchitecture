using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.Dto.Iot;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Devices.Queries.GetDevices
{
    public class GetDevicesQuery : IRequest<DevicesVm> { }

    public class GetDevicesQueryHandler : IRequestHandler<GetDevicesQuery, DevicesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDevicesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DevicesVm> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
        {
            var devices = new DevicesVm
            {
                Lists = await _context.Devices
                   .ProjectTo<DeviceDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.Name)
                   .ToListAsync(cancellationToken)
            };

            return devices;
        }
    }
}
