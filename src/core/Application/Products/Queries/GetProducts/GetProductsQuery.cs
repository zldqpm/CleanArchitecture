using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.Dto.Iot;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<ProductsVm> { }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductsVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = new ProductsVm
            {
                Lists = await _context.Products
                   .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.Name)
                   .ToListAsync(cancellationToken)
            };
            return products;
        }
    }
}
