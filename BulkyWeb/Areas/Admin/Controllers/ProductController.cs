using BookNest.DataAccess.Repository;
using BookNest.DataAccess.Repository.IRepository;
using BookNest.Models;
using BookNest.Models.ViewModels;
using BookNest.Utility;
using BookNestWeb.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookNestWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        // If getting error then check Service/ create interface object
        private readonly IUnitOfWork _unitOfWork;
        //This is access the www.root folder in our project
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
		public IActionResult Index()
		{
			// Use "Category" to include the navigation property instead of "CategoryId"
			List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

			return View(productList);
		}
		// Upsert means update insert both 
		public IActionResult Upsert(int? id) 
        {
			// This using projection this feautures only retrive some columns not all columns
			//IEnumerable<SelectListItem> CategoryList = 
            //ViewBag takes Dynamic values that advantage
            //ViewBag.CategoryList = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.CategoryId.ToString(),
				}),
				Product = new Product()
            };
            if(id == null || id==0)
            {
                //Create
				return View(productVM);
            }
            else
            {
                //Update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
		}
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                //This is storing the path of wwwroot folder
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    // this is giving random name of file
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    //chaking image url is null or not
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //Delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        //anything is exits in oldimagepath then we will delete
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    // Upload the image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create)) 
                    {
                        //this line copy the file in the new location
                        file.CopyTo(fileStream);
                    }
                    // Update the image url
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if (productVM.Product.Id == 0)
                {
					_unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product has been created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString(),
                });
				return View(productVM);

			}
		}
        public IActionResult Delete(int? id)
        {
            if (id == null || id==0)
            {
                NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u=> u.Id == id);
            if (productFromDb == null)
            {
                NotFound();
            }
            return View(productFromDb);
        }

        [HttpDelete]
        public IActionResult DeletePost(int? id)
        {
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            //Delete the old image
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,product.ImageUrl.TrimStart('\\'));
            //anything is exits in oldimagepath then we will delete
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
			//TempData["success"] = "Product has been deleted successfully";
			return Json(new { success = true, message = "Product has been deleted successfully." });
			//return RedirectToAction("Index");
        }
    }
}
