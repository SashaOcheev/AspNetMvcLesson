using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;

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
            var cars = _carsRepository.GetAll();
            return View( cars );
        }
    }
}
