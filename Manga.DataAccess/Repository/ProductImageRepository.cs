using System;
using Manga.DataAccess.Data;
using Manga.DataAccess.Repository.IRepository;
using Manga.Models;

namespace Manga.DataAccess.Repository
{
    public class ProductImageRepostory : Repository<ProductImage>, IProductImageRepostory
    {
        private ApplicationDbContext _db;
        public ProductImageRepostory(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(ProductImage obj)
        {
            _db.ProductImages.Update(obj);
        }

        
    }
}

