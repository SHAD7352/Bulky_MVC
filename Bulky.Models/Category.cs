﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		[Required]
		[DisplayName("Category Name")]
		[MaxLength(30)]
		public string Name { get; set; }
		[DisplayName("Display Order")]
		[Range(1, 100, ErrorMessage = "Display Order must be 1-100")]
		public int DisplayOrder { get; set; }
	}
}



