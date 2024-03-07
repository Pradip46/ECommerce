using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerceWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        //Get Create
        public IActionResult Create()
        {

            return View();
        }
        //Post Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.DisplayOrder.ToString() == obj.Name)
            {

                ModelState.AddModelError("DisplayOrder", "Display order cannot be same as Name.");
            }

            //bool IfExits = _db.Categories.Any(x => string.Equals(x.Name, obj.Name, StringComparison.Ordinal));

            //if ( IfExits == true)
            //{
            //    ModelState.AddModelError("Name","Category name is Already exits!! PLease create different Category namme");

            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category is Created Successfully";

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
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);



        }
        //Post Edit
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.DisplayOrder.ToString() == obj.Name)
            {
                ModelState.AddModelError("DisplayOrder", "Display order cannot be same as Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                TempData["success"] = "Category is updated Successfully";
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
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);



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
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.Find(id);


            _unitOfWork.Category.Remove(categoryFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Category is deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
