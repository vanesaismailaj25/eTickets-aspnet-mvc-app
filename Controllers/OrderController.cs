﻿using eTickets.Data.Cart;
using eTickets.Data.Services.IServices;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        public OrderController(IMovieService movieService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;

        }

        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await _orderService.GetOrdersByIdAsync(userId);

            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _movieService.GetMovieByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _movieService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            string userId = "";
            string userEmailAddress = "";

            await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}