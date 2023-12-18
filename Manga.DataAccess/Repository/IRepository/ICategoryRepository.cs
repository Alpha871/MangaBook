using System;
using Manga.Models;

namespace Manga.DataAccess.Repository.IRepository
{
	public interface ICategoryRepository:IRepository<Category>
	{
		void Update(Category obj);
	}
}

