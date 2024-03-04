using eTickets.Context;
using eTickets.Data.Services.IServices;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    public OrderService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Order>> GetOrdersByIdAndRoleAsync(string userId, string userRole)
    {
        var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Include(n => n.User).ToListAsync();

        if(userRole != "Admin")
        {
            orders = orders.Where(n=> n.UserId == userId).ToList();
        }

        return orders;
    }

    public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
    {
        var order = new Order()
        {
            UserId = userId,
            Email = userEmailAddress,
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach (var item in items)
        {
            var orderItem = new OrderItem()
            {
                Amount = item.Amount,
                MovieId = item.Movie.Id,
                Price = item.Movie.Price,
                OrderId = item.Id
            };
            await _context.AddAsync(orderItem);
        }
        await _context.SaveChangesAsync();
    }
}
