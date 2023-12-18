using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manga.Models;
using Microsoft.AspNetCore.Mvc;
using Manga.DataAccess.Data;
using Manga.DataAccess.Repository.IRepository;
using Manga.Utility;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MangaWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Commun.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            if(id== 0 || id==null)
            {
                return View(new Company());
            }

            var obj = _unitOfWork.Company.Get(u => u.Id == id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Upsert(Company obj)
        {
            

            if (ModelState.IsValid)
            {

                if(obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                }

                
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index", "Company");
            }

            return View();

        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Company> companyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);

            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Company.Remove(companyToBeDeleted)
;
            _unitOfWork.Save();


            return Json(new { success = false, message = "Deleted Successfully" });
        }


        #endregion
    }
}



