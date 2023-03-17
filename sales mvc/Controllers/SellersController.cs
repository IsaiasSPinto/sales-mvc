using Microsoft.AspNetCore.Mvc;
using sales_mvc.Models;
using sales_mvc.Models.ViewModels;
using sales_mvc.Services;
using sales_mvc.Services.Exeptions;
using System.Diagnostics;

namespace sales_mvc.Controllers {
    public class SellersController : Controller {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index() {
            var sellers = _sellerService.FindAll();

            return View(sellers);
        }

        public IActionResult Create() {
            var departments = _departmentService.FindAll();

            var viewModel = new SellerFormViewModel { 
                Departments = departments
            };


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            _sellerService.Insert(seller);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id) {
            if (id == null) {
                return RedirectToAction("Error", new { message = "Id not Provided"});
            }

            var seller = _sellerService.FindById(id);

            if (seller == null) {
                return RedirectToAction("Error", new { message = "Id not Found"});
            }

            return View(seller);
        }



        public IActionResult Delete(int? id) {
            if(id == null) {
                return RedirectToAction("Error", new { message = "Id not Provided" });
            }

            var seller = _sellerService.FindById(id);

            if(seller == null) {
                return RedirectToAction("Error", new { message = "Id not Found" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            _sellerService.Remove(id);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction("Error", new { message = "Id not Provided" });
            }

            var seller = _sellerService.FindById(id);

            if (seller == null) {
                return RedirectToAction("Error", new { message = "Id not Found" });
            }

            var departments = _departmentService.FindAll();

            var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Seller seller) {
            try {

                _sellerService.Update(seller);

                return RedirectToAction("Index");

            } catch (ApplicationException e) {
                return RedirectToAction("Error", new { message = e.Message });
            }
        }

        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
