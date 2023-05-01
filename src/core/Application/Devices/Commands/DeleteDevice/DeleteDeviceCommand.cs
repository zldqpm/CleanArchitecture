﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Devices.Commands.DeleteDevice
{
    public class DeleteDeviceCommand : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteDeviceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Devices
              .Where(l => l.Id == request.Id)
              .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Products), request.Id);
            }

            _context.Devices.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
