﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IProductRepository : IGenericRepository<Product> 
    {
        Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
    }
}
