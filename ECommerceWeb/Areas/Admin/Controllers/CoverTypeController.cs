using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerceWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }
        //Get Create
        public IActionResult Create()
        {

            return View();
        }
        //Post Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CoverType obj)
        {


            //bool IfExits = _db.Categories.Any(x => string.Equals(x.Name, obj.Name, StringComparison.Ordinal));

            //if ( IfExits == true)
            //{
            //    ModelState.AddModelError("Name","Category name is Already exits!! PLease create different Category namme");

            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType is Created Successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.Find(id);
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);



        }
        //Post Edit
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                TempData["success"] = "CoverType is updated Successfully";
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var CoverTypeFromDb = _db.CoverTypes.Find(id);
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);



        }
        //Post Delete
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePOST(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.Find(id);


            _unitOfWork.CoverType.Remove(CoverTypeFromDb);
            _unitOfWork.Save();
            TempData["success"] = "CoverType is deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
