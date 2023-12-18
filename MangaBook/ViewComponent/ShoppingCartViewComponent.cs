using System;
using System.Security.Claims;
using Manga.DataAccess.Repository.IRepository;
using Manga.Utility;
using Microsoft.AspNetCore.Mvc;
namespace MangaWEB.ViewComponents
{
	public class ShoppingCartViewComponent: ViewComponent
    {
		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst((ClaimTypes.NameIdentifier));

            if (claim != null)
            {
                if(HttpContext.Session.GetInt32(Commun.SessionCart)!=null)
                {
                    HttpContext.Session.SetInt32(Commun.SessionCart,
                   _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
                }
               
                return View(HttpContext.Session.GetInt32(Commun.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
	}
}

