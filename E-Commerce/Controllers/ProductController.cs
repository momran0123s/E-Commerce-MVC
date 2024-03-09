using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private CommerceContext db;
        private readonly UserManager<ApplicationUser> userManager;
        public ProductController(CommerceContext _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {

            List<Product> products =  db.Products.ToList();
            return View(products);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                Product product = db.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(); // Product not found
                }

                Cart cart = db.Carts.FirstOrDefault(c => c.UserId == user.Id);
                if (cart == null)
                {
                    cart = new Cart()
                    {
                        UserId = user.Id,
                    };
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }

                // Check if the product is already in the cart
                CartItems existingCartItem = db.CartItems.FirstOrDefault(ci => ci.CartId == cart.Id && ci.ProductId == id);
                if (existingCartItem != null)
                {
                    // Update quantity or any other relevant properties
                    existingCartItem.ProductQuantity++; // For example, increment quantity
                    existingCartItem.TotalPrice += product.Price; // Adjust total price
                }
                else
                {
                    // Create a new CartItem if it doesn't exist
                    CartItems cartItems = new CartItems()
                    {
                        CartId = cart.Id,
                        ProductQuantity = 1,
                        ProductId = id,
                        TotalPrice = product.Price,
                    };
                    db.CartItems.Add(cartItems);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return Content("Sign in first");
        }

    }
}
