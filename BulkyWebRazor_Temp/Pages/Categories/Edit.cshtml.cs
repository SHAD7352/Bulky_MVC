using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;

namespace BulkyWebRazor_Temp.Pages.Categories
{
	[BindProperties]
	public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

		public Category Category { get; set; }
		public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
			    Category = _db.categories.Find(id);
            }
        }
        public IActionResult OnPost(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category is updated successfully";
            }
			return RedirectToPage("index");

		}
	}
}
