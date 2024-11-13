using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace TestTask.Services.Implementations
{
    public class UserServices : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<User> IUserService.GetUser()
        {

            var userWithMaxWaste = await _context.Users
                .Where(o => o.Orders.Any(o => o.CreatedAt.Year == 2003))
                .Select(user => new
                {
                    id = user.Id,
                    waste = user.Orders.Sum(o => o.Price),
                })
                .OrderByDescending(u => u.waste)
                .FirstOrDefaultAsync();

            return await _context.Users.FindAsync(userWithMaxWaste.id);
        }

        async Task<List<User>> IUserService.GetUsers()
        {

            return await _context.Users
                .Where(o => o.Orders.Any(s => s.Status == OrderStatus.Paid))
                .ToListAsync();
            //
        }
    }
}
