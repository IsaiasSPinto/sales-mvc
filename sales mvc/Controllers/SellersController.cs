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

        public async Task<IActionResult> Index() {
            var sellers = await _sellerService.FindAllAsync();

            return View(sellers);
        }

        public async Task<IActionResult> Create() {
            var departments = await _departmentService.FindAllAsync();

            var viewModel = new SellerFormViewModel { 
                Departments = departments
            };


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller) {
            if(!ModelState.IsValid) {
                var departments = await _departmentService.FindAllAsync();

                var viewModel = new SellerFormViewModel {
                    Seller = seller,
                    Departments = departments
                };

                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return RedirectToAction("Error", new { message = "Id not Provided"});
            }

            var seller = await _sellerService.FindByIdAsync(id);

            if (seller == null) {
                return RedirectToAction("Error", new { message = "Id not Found"});
            }

            return View(seller);
        }



        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return RedirectToAction("Error", new { message = "Id not Provided" });
            }

            var seller = await _sellerService.FindByIdAsync(id);

            if(seller == null) {
                return RedirectToAction("Error", new { message = "Id not Found" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {

            try {

                await _sellerService.RemoveAsync(id);

            } catch (IntegrityException e) {

                RedirectToAction("Error", new { message = e.Message });
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return RedirectToAction("Error", new { message = "Id not Provided" });
            }

            var seller = await _sellerService.FindByIdAsync(id);

            if (seller == null) {
                return RedirectToAction("Error", new { message = "Id not Found" });
            }

            var departments = await _departmentService.FindAllAsync();

            var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Seller seller) {
            try {
                if(!ModelState.IsValid) {
                    var departments = await _departmentService.FindAllAsync();

                    var viewModel = new SellerFormViewModel {
                        Seller = seller,
                        Departments = departments
                    };

                    return View(viewModel);
                }

                await _sellerService.UpdateAsync(seller);

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
