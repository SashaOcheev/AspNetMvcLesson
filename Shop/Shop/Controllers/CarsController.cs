using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsRepository _carsRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public CarsController( ICarsRepository carsRepository, ICategoriesRepository categoriesRepository )
        {
            _carsRepository = carsRepository;
            _categoriesRepository = categoriesRepository;
        }

        public ViewResult CarsList()
        {
            ViewBag.Category = "Some New"; // Вместо Category может быть что угодно. Но, сем меньше таких штук, тем лучше
            var cars = _carsRepository.GetAll();
            return View( cars );
        }

        public ViewResult List()
        {
            ViewBag.Title = "Страница с автомобилями";
            var carsViewModel = new CarsListViewModel();
            carsViewModel.AllCars = _carsRepository.GetAll();
            carsViewModel.CurrentCategory = "Автомобили";
            return View( carsViewModel );
        }
    }
}
