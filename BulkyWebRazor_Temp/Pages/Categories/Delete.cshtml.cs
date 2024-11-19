using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;   
        }

        public void OnGet(int? id)
        {
            if(id!= null && id!=0)
            {
                category = _db.categories.Find(id);
            }
        }

        public IActionResult OnPost(int? id)
        {
			category = _db.categories.Find(id);
            if(category!=null)
            {
				_db.categories.Remove(category);
				_db.SaveChanges();
                TempData["success"]="Category is deleted successfully";
			}
            return RedirectToPage("index");
        }
    }
}
