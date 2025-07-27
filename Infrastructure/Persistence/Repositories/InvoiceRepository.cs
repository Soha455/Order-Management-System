using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly OrderManagmentDbContext _context;

        public InvoiceRepository(OrderManagmentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Invoice?> GetInvoiceByOrderIdAsync(int orderId)
        {
            return await _context.Invoices
                .FirstOrDefaultAsync(i => i.OrderId == orderId);
        }
    }
}
