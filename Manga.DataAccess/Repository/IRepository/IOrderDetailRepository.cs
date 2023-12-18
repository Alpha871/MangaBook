using System;
using Manga.Models;

namespace Manga.DataAccess.Repository.IRepository
{
	public interface IOrderDetailRepository:IRepository<OrderDetail>
	{
		void Update(OrderDetail obj);
	}
}

