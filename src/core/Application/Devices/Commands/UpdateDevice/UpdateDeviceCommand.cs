using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Devices.Commands.UpdateDevice
{
    public class UpdateDeviceCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Secret { get; set; }
    }

    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDeviceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Devices.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Devices), request.Id);
            }
            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.Secret = request.Secret;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
