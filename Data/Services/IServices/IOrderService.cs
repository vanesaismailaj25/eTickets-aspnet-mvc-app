using eTickets.Models;

namespace eTickets.Data.Services.IServices;

public interface IOrderService
{
    Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
    Task<List<Order>> GetOrdersByIdAsync(string userId);
}
