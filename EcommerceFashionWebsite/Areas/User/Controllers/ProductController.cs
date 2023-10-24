using EcommerceFashionWebsite.Areas.Admin.ViewModels;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.ViewComponentsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommerceFashionWebsite.Areas.User.Controllers
{
	[Area("User")]
	public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var productModel = _db.ProductModel.Find(id);

           List<int> categoryIds =  _db.ProductCategoryModel
                .Where(pc => pc.ProductId == id)
                .Select(pc => pc.CategoryId)
                .ToList();

            var product = new ProductViewModel
            {
                ProductId = productModel.Id,
                ProductName = productModel.Name,
                ProductDescription = productModel.Desc,

                ImageUrls = _db.ImageModel
                     .Where(img => img.ProductId == id)
                     .Select(img => img.ImageUrl)
                     .ToList(),
                CategoryNames = _db.ProductCategoryModel
                    .Where(pc => pc.ProductId == id)
                    .Select(pc => pc.Category.Name)
                    .ToList(),
                PriceApply = (int)productModel.PriceApply,
                PriceOrigin = (int)productModel.PriceOrigin,
                Sizes = productModel.Sizes,
                NumberInStock = productModel.NumberInStock,
                Tags = productModel.Tags,
                Introduction = productModel.Introduction,
                Features = productModel.Features,
                Colors = productModel.Colors,
                RelatedProducts = _db.ProductCategoryModel
                .Where(pc => pc.ProductId != id && categoryIds.Contains(pc.CategoryId))
                .Select(pc => new ProductViewModel
                {
                    ProductId = pc.Product.Id,
                    ProductName = pc.Product.Name,
                    PriceOrigin = (int)pc.Product.PriceOrigin,
                    PriceApply = (int)pc.Product.PriceApply,
                    StarRating = pc.Product.StarRating,
                    NumberInStock = pc.Product.NumberInStock,
                    ImageUrls = new List<string>
                    {
                        _db.ImageModel
                         .Where(img => img.ProductId == pc.Product.Id)
                         .Select(img => img.ImageUrl)
                         .FirstOrDefault()
                    }
                })
                .ToList(),
                StarRating = productModel.StarRating
        };

            return View(product);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Detail(ProductViewModel productViewModel)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            CartModel cartModel = new CartModel()
            {
                ProductId = productViewModel.ProductId,
                Quantity = productViewModel.Quantity,
                ApplicationUserId = claim.Value
            };

            //Them san pham vao gio hang
            _db.CartModel.Add(cartModel);
            _db.SaveChanges();

            return Json(new { success = true });

        }

        public IActionResult Search(string query, int categoryId)
        {
            if (categoryId != 0)
            {
                var products = _db.ProductModel
                   .Include(p => p.Categories)
                   .ThenInclude(pc => pc.Category)
                   .Where(p => p.Categories.Any(pc => pc.Category.Id == categoryId))
                   .Select(p => new SearchResultModel
                   {
                       Id = p.Id,
                       Name = p.Name,
                       UrlImage = _db.ImageModel
                                                 .Where(img => img.ProductId == p.Id)
                                                 .Select(img => img.ImageUrl)
                                                 .FirstOrDefault(),
                       StarRating = p.StarRating
                   })
                   .ToList();

                return Json(products);
            } else
            {
                var products = _db.ProductModel
                                       .Where(p => p.Name.Contains(query))
                                       .Select(p => new SearchResultModel
                                       {
                                           Id = p.Id,
                                           Name = p.Name,
                                           UrlImage = _db.ImageModel
                                                 .Where(img => img.ProductId == p.Id)
                                                 .Select(img => img.ImageUrl)
                                                 .FirstOrDefault(),
                                           StarRating = p.StarRating
                                       })
                                       .ToList();
            return Json(products);

            }

        }
    }
}
