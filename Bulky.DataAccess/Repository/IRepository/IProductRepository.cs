using BookNest.Models;
using BookNestWeb.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {

        // The Update method is specific to the Product entity. It is not included in IRepository because:
        //Not All Entities Need Update: For some entities, you may not need an Update method.
        void Update(Product product);
    }
}
