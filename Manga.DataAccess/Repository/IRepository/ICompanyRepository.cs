using System;
using Manga.Models;

namespace Manga.DataAccess.Repository.IRepository
{
	public interface ICompanyRepository:IRepository<Company>
	{
		void Update(Company company);
	}
}

