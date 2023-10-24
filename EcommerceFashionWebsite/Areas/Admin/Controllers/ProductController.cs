using EcommerceFashionWebsite.Areas.Admin.ViewModels;
using EcommerceFashionWebsite.Data;
using EcommerceFashionWebsite.Interfaces;
using EcommerceFashionWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceFashionWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPhotoService _photoService;
        public ProductController(ApplicationDbContext db, IPhotoService photoService)
        {
            _db = db;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            var products = _db.ProductModel
        .Include(p => p.Images)
        .Include(p => p.Categories)
        .ThenInclude(pc => pc.Category)
        .ToList();

            var productViewModels = products.Select(product => new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductDescription = product.Desc,
                ImageUrls = _db.ImageModel
                        .Where(img => img.ProductId == product.Id)
                        .Select(img => img.ImageUrl)
                        .ToList(),
                CategoryNames = product.Categories?.Select(pc => pc.Category?.Name).ToList() ?? new List<string>(),
                PriceApply = (int)product.PriceApply,
                PriceOrigin = (int)product.PriceOrigin,
                Sizes = product.Sizes,
                NumberInStock = product.NumberInStock,
                Tags = product.Tags,
                Introduction = product.Introduction,
                Features = product.Features,
                Colors = product.Colors,
                Desc = product.Desc
                // Các thuộc tính khác của ProductViewModel bạn muốn hiển thị
            }).ToList();

            return View(productViewModels);
        }

        public IActionResult Add()
        {
            var categories = _db.CategoryModel.ToList();
            var productVM = new CreateProductViewModel
            {
                CategoriesSelectList = categories
            .Select(category => new SelectListItem
            {
                Text = category.Name, // Thay "Name" bằng thuộc tính trong CategoryModel bạn muốn hiển thị
                Value = category.Id.ToString() // Thay "Id" bằng thuộc tính ID trong CategoryModel
            })
            .ToList()
            };
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateProductViewModel productVM)
        {
            
                var productModel = new ProductModel
                {
                    Name = productVM.Name,
                    Desc= productVM.Desc,
                    Introduction= productVM.Introduction,
                    Sizes = productVM.Sizes,
                    Colors = productVM.Colors,
                    Tags = productVM.Tags,
                    NumberInStock = productVM.NumberInStock,
                    PriceApply = (float)productVM.PriceApply,
                    PriceOrigin = (float)productVM.PriceOrigin,
                    Features = productVM.Features
                };
                _db.ProductModel.Add(productModel);
                await _db.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

                // Sau khi lưu thay đổi, productModel.Id sẽ chứa ID của đối tượng mới được tạo.
                int newProductId = productModel.Id;


                // Sử dụng lập trình không đồng bộ để tải lên hình ảnh đồng thời
                var uploadTasks = productVM.Images.Select(async image =>
                {
                    var result = await _photoService.AddPhotoAsync(image);
                    // Xử lý kết quả ở đây, ví dụ: lưu trữ thông tin kết quả vào cơ sở dữ liệu
                    // Lưu trữ kết quả vào ImageModel với trường ImageUrl
                    var imageModel = new ImageModel
                    {
                        ProductId = newProductId,
                        ImageUrl = result.Url.ToString()
                    };
                    // Lưu trữ imageModel vào cơ sở dữ liệu
                    // dbContext.ImageModels.Add(imageModel);
                    // await dbContext.SaveChangesAsync(); // Lưu trữ vào cơ sở dữ liệu

                    return imageModel; // Trả về imageModel nếu bạn muốn lưu trữ chúng
                });

                // Đợi cho tất cả các yêu cầu API hoàn thành và lấy kết quả trả về từ mỗi yêu cầu
                var imageModels = await Task.WhenAll(uploadTasks);

                // Bây giờ, biến imageModels chứa một mảng các ImageModel với trường ImageUrl được lưu trữ.
                // Lưu vào cơ sở dữ liệu sử dụng Entity Framework Core
                _db.ImageModel.AddRange(imageModels);

                var productCategoryModels = productVM.SelectedCategories.Select(id =>
                {
                    var productCategoryModel = new ProductCategoryModel
                    {
                        ProductId = newProductId,
                        CategoryId = Int32.Parse(id)
                    };

                    return productCategoryModel;
                });

                _db.ProductCategoryModel.AddRange(productCategoryModels);

                await _db.SaveChangesAsync(); // Lưu trữ vào cơ sở dữ liệu
                return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var productModel = _db.ProductModel.Find(id);

            var categories = _db.CategoryModel.ToList();
            var productVM = new CreateProductViewModel
            {
                Id = productModel.Id,
                CategoriesSelectList = categories
                .Select(category => new SelectListItem
                {
                    Text = category.Name, // Thay "Name" bằng thuộc tính trong CategoryModel bạn muốn hiển thị
                    Value = category.Id.ToString() // Thay "Id" bằng thuộc tính ID trong CategoryModel
                })
                .ToList(),
                SelectedCategories = _db.ProductCategoryModel
                .Where(pc => pc.ProductId == id)
                .Select(pc => pc.CategoryId.ToString())
                .ToList(),
                Name = productModel.Name,
                ImageUrls = _db.ImageModel
                        .Where(img => img.ProductId == id)
                        .Select(img => img.ImageUrl)
                        .ToList(),
                Desc = productModel.Desc,
                PriceApply = (int)productModel.PriceApply,
                PriceOrigin = (int)productModel.PriceOrigin,
                Sizes = productModel.Sizes,
                NumberInStock = productModel.NumberInStock,
                Tags = productModel.Tags,
                Introduction = productModel.Introduction,
                Features = productModel.Features,
                Colors = productModel.Colors
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(CreateProductViewModel productVM)
        {

            var productModel = new ProductModel
            {
                Id = productVM.Id,
                Name = productVM.Name,
                Desc = productVM.Desc,
                Introduction = productVM.Introduction,
                Sizes = productVM.Sizes,
                Colors = productVM.Colors,
                Tags = productVM.Tags,
                NumberInStock = productVM.NumberInStock,
                PriceApply = (float)productVM.PriceApply,
                PriceOrigin = (float)productVM.PriceOrigin,
                Features = productVM.Features
            };
            _db.ProductModel.Update(productModel);
            await _db.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            // Sau khi lưu thay đổi, productModel.Id sẽ chứa ID của đối tượng mới được tạo.
           


            if (productVM.Images != null && productVM.Images.Count > 0)
            {
                // chắc chắn user đang chọnc ác hình ảnh mới thay thế
                // Đầu tiên sẽ xóa tất cả các hình ảnh có liên quan
                var imageModelsDelete = _db.ImageModel
                        .Where(img => img.ProductId == productVM.Id)
                        .ToList();

                _db.ImageModel.RemoveRange(imageModelsDelete);

                // Sử dụng lập trình không đồng bộ để tải lên hình ảnh đồng thời
                var uploadTasks = productVM.Images.Select(async image =>
                {
                    var result = await _photoService.AddPhotoAsync(image);
                    // Xử lý kết quả ở đây, ví dụ: lưu trữ thông tin kết quả vào cơ sở dữ liệu
                    // Lưu trữ kết quả vào ImageModel với trường ImageUrl
                    var imageModel = new ImageModel
                    {
                        ProductId = productVM.Id,
                        ImageUrl = result.Url.ToString()
                    };
                    // Lưu trữ imageModel vào cơ sở dữ liệu
                    // dbContext.ImageModels.Add(imageModel);
                    // await dbContext.SaveChangesAsync(); // Lưu trữ vào cơ sở dữ liệu

                    return imageModel; // Trả về imageModel nếu bạn muốn lưu trữ chúng
                });

                // Đợi cho tất cả các yêu cầu API hoàn thành và lấy kết quả trả về từ mỗi yêu cầu
                var imageModels = await Task.WhenAll(uploadTasks);

                // Bây giờ, biến imageModels chứa một mảng các ImageModel với trường ImageUrl được lưu trữ.
                // Lưu vào cơ sở dữ liệu sử dụng Entity Framework Core
                _db.ImageModel.AddRange(imageModels);

            }


            //// Tìm ProductModel để xóa
            //var product = _db.ProductModel
            //    .Include(p => p.Images)
            //    .Include(p => p.Categories)
            //    .SingleOrDefault(p => p.Id == newProductId);

            //_db.ProductCategoryModel.RemoveRange(product.Categories);

            //// xóa categỏy xong tạo lại

            //var productCategoryModels = productVM.SelectedCategories.Select(id =>
            //{
            //    var productCategoryModel = new ProductCategoryModel
            //    {
            //        ProductId = newProductId,
            //        CategoryId = Int32.Parse(id)
            //    };

            //    return productCategoryModel;
            //});

            //_db.ProductCategoryModel.AddRange(productCategoryModels);


            // Lấy danh sách các thể loại hiện tại của sản phẩm từ cơ sở dữ liệu
            var currentCategories = _db.ProductCategoryModel
    .Where(pc => pc.ProductId == productVM.Id)
    .Select(pc => pc.CategoryId)
    .ToList();

            
            // So sánh danh sách thể loại hiện tại với danh sách được chọn mới:
            var selectedCategories = productVM.SelectedCategories.Select(id => Int32.Parse(id)).ToList();

            var categoriesToAdd = selectedCategories.Except(currentCategories).ToList();
            var categoriesToRemove = currentCategories.Except(selectedCategories).ToList();


            // Thêm thể loại mới và xóa thể loại không chọn:
            var productCategoryModelsToAdd = categoriesToAdd.Select(categoryId => new ProductCategoryModel
            {
                ProductId = productVM.Id,
                CategoryId = categoryId
            });

            var productCategoryModelsToRemove = _db.ProductCategoryModel
                .Where(pc => pc.ProductId == productVM.Id && categoriesToRemove.Contains(pc.CategoryId))
                .ToList();

            _db.ProductCategoryModel.AddRange(productCategoryModelsToAdd);
            _db.ProductCategoryModel.RemoveRange(productCategoryModelsToRemove);

            await _db.SaveChangesAsync(); // Lưu trữ vào cơ sở dữ liệu
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Tìm ProductModel để xóa
            var product = _db.ProductModel
                .Include(p => p.Images)
                .Include(p => p.Categories)
                .SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); // Trả về NotFound nếu không tìm thấy ProductModel
            }

            // Xóa ImageModels dựa trên ProductId
            var imageModels = _db.ImageModel
                        .Where(img => img.ProductId == product.Id)
                        .ToList();

            _db.ImageModel.RemoveRange(imageModels);

            // Xóa ProductCategoryModels dựa trên ProductId
            _db.ProductCategoryModel.RemoveRange(product.Categories);

            // Xóa ProductModel
            _db.ProductModel.Remove(product);

            // Lưu thay đổi vào cơ sở dữ liệu
            _db.SaveChanges();

            return RedirectToAction("Index"); // Chuyển hướng về trang danh sách sản phẩm hoặc trang khác tùy ý của bạn
        }
    }
}
