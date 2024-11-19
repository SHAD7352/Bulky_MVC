using BookNest.DataAccess.Repository.IRepository;
using BookNest.Models;
using BookNestWeb.DataAccess.Data;
using BookNestWeb.DataAccess.Repository;
using BookNestWeb.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.DataAccess.Repository
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db=db;
        }
        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);
            if (objFromDb != null)
            {
				objFromDb.Title = product.Title;
				objFromDb.Description = product.Description;
				objFromDb.ISBN = product.ISBN;
				objFromDb.Category = product.Category;
				objFromDb.Author = product.Author;
				objFromDb.ListPrice = product.ListPrice;
				objFromDb.Price = product.Price;
				objFromDb.Price50 = product.Price50;
				objFromDb.Price100 = product.Price100;
				objFromDb.CategoryId = product.CategoryId;

				if (product.ImageUrl != null) 
                {
                    objFromDb.ImageUrl = product.ImageUrl; 
                }
               
            }
            //_db.Products.Update(objFromDb);
        }
    }
}
