using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookNestWeb.DataAccess.Data;
using BookNestWeb.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookNestWeb.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
			_db = db;
			this.dbSet = _db.Set<T>();
			//_db.Categories == dbSet;
			_db.Products.Include(u => u.Category).Include(u=>u.CategoryId);
		}
        public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				// Remove to empty entry
				foreach (var incluproperty in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(incluproperty);
				}
			}
			query = query.Where(filter);
			return query.FirstOrDefault();

		}

		// Category , CoverType
		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				// Remove to empty entry
				foreach (var incluproperty in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(incluproperty);
				}
			}
			return query.ToList(); 
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);									;
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}
