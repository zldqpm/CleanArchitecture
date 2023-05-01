﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Command.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Secret { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Products), request.Id);
            }
            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.Secret = request.Secret;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
