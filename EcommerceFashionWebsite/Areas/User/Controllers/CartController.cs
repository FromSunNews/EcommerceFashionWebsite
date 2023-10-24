using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
    
	public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Details(int ProductId)

        {
            CartModel cart = new CartModel()
            {
                ProductId = ProductId,
                ProductModel = _db.ProductModel.Where(p => p.Id == ProductId).FirstOrDefault(),
                Quantity = 1

            };
            return View(cart);
        }
    }
}
