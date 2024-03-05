﻿using eTickets.Context;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart;

public class ShoppingCart
{
    public AppDbContext _context { get; set; }
    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    public ShoppingCart(AppDbContext context)
    {
        _context = context;
    }

    public static ShoppingCart GetShoppingCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<AppDbContext>();

        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        session.SetString("CartId", cartId);

        return new ShoppingCart(context)
        {
            ShoppingCartId = cartId
        };
    }

    public void AddItemToCart(Movie movie)
    {
        var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem()
            {
                ShoppingCartId = ShoppingCartId,
                Amount = 1,
                Movie = movie
            };
            _context.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }

        _context.SaveChanges();
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(m => m.Movie).ToList());
    }

    public double GetShoppingCartTotal()
    {
        return _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
    }

    public void RemoveItemFromCart(Movie movie)
    {
        var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem != null)
        {
            if(shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }      
        _context.SaveChanges();
    }

    public async Task ClearShoppingCartAsync()
    {
        var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
        _context.ShoppingCartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
        //to hide the shopping cart icon after an order is completed 
        ShoppingCartItems = new List<ShoppingCartItem>();
    }
}
