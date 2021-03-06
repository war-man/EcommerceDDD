﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDDD.Domain.Products;
using EcommerceDDD.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDDD.Infrastructure.Domain.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDDDContext _context;

        public ProductRepository(EcommerceDDDContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);            
        }

        public async Task AddRange(List<Product> products, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddRangeAsync(products, cancellationToken);
        }

        public async Task<Product> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Product>> GetByIds(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> ListAll(CancellationToken cancellationToken = default)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
    }
}
