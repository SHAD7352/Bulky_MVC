using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookNestWeb.DataAccess.Repository.IRepository
{
	// This is all called generics interface
	public interface IRepository<T> where T : class // T means it can work any type of object like product and  category
													// (T:class) this is constrains T must be class rather than primitive data type like string or int
	{
		IEnumerable<T> GetAll(string? includeProperties = null);
		//this is lamda expression
		T Get(Expression <Func<T, bool>> filter, string? includeProperties = null);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
	}
}
