using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        private CommerceContext db;
        private readonly UserManager<ApplicationUser> userManager;
        public CartController(CommerceContext _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                Cart cart = db.Carts.SingleOrDefault(c=>c.UserId == user.Id);
                if (cart != null)
                {
                List<CartItems> items = db.CartItems.Where(i=>i.CartId == cart.Id).ToList();

                var totalCartPrice = db.CartItems.Where(x=>x.CartId == cart.Id).Select(p=>p.TotalPrice).Sum();
                ViewBag.TotalCartPrice = totalCartPrice;
                ViewBag.CartId = cart.Id;
                return View(items);
                }
                else
                {
                    return View(null);
                }

            }
            return RedirectToAction("Login","Login");
        }

        public IActionResult EmptyCart(int id)
        {
            var cart = db.Carts.SingleOrDefault(i => i.Id == id);
            if (cart != null)
            {

            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            return Content("cart is null");
        }

        public IActionResult Delete(int id)
        {
            CartItems items = db.CartItems.SingleOrDefault(x=>x.Id == id);
            if (items != null)
            {
                db.CartItems.Remove(items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("no more");
            }
        }
    }
}
