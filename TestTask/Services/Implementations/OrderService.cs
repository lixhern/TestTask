using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
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


            return await _context.Orders
                .Where(o => o.Quantity > 1)
                .OrderByDescending(o => o.CreatedAt)
                .FirstAsync();

        }

        async Task<List<Order>> IOrderService.GetOrders()
        {
            return await _context.Orders
                .Where(u => u.User.Status == UserStatus.Active)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }
    }
}
