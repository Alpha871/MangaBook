using System;
using Manga.Models;

namespace Manga.DataAccess.Repository.IRepository
{
	public interface IProductRepository:IRepository<Product>
	{
		void Update(Product product);
	}
}

