using System;
using Manga.Models;


namespace Manga.DataAccess.Repository.IRepository
{
	public interface IShoppingCartRepository:IRepository<ShoppingCart>
	{
		void Update(ShoppingCart obj);
	}
}

