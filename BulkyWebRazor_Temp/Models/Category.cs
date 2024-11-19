using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BulkyWebRazor_Temp.Models
{
	public class Category
	{
		[Key]
		public int id { get; set; } 
		[Required]
		[DisplayName("Category Name")]
		[MaxLength(30)]
		public string? Name { get; set; }
		[DisplayName("Category Order")]
		[Range(1,100,ErrorMessage = "Display Order must be 1-100")]
		public int CategoryOrder { get; set; }
	}
}
