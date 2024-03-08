using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ECommerceWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
            return View(objProductList);
        }
       

        //Get Upsert
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    })

            };

            if (id == null || id == 0)
            {
                //Create product
                
                return View(productVM);
            }
            else
            {
                //update product
                
                

            }
            return View(productVM);




        }
        //Post Upsert
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(CoverType obj)
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
