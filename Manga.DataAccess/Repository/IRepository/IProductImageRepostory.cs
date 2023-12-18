using System;
using Manga.Models;

namespace Manga.DataAccess.Repository.IRepository
{
	public interface IProductImageRepostory : IRepository<ProductImage>
	{
		void Update(ProductImage obj);
	}
}

