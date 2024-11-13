using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<Order> IOrderService.GetOrder()
        {
            return await _context.Orders.FirstOrDefaultAsync();
        }

        async Task<List<Order>> IOrderService.GetOrders()
        {
            return await _context.Orders
                .Where(u => u.User.Id == 1)
                .ToListAsync();
        }
    }
}
