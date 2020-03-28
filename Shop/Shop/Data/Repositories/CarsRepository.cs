using Shop.Data.Interfaces;
using Shop.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Data.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly ICategoriesRepository _categoriesRepository = new CategoriesRepository();

        public CarsRepository( ICategoriesRepository categoriesRepository )
        {
            _categoriesRepository = categoriesRepository;
        }

        public IEnumerable<Car> GetAll()
        {
            return new List<Car>
            {
                new Car
                {
                    Name = "Tesla Model S",
                    ShortDescription = "Быстрый автомобиль",
                    LongDescription = "Красивый, быстрый и очень тихий автомобиль комании Tesla",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/Tesla_Model_S_02_2013.jpg/1280px-Tesla_Model_S_02_2013.jpg",
                    Price = 45000,
                    IsFavourite = true,
                    IsAvailable = true,
                    Category = _categoriesRepository.GetAll().First()
                },
                new Car
                {
                    Name = "Ford Fiesta",
                    ShortDescription = "Тихий и спокойный",
                    LongDescription = "Удобный автомобиль для городской жизни",
                    ImageUrl = "/img/ford.webp",
                    Price = 11000,
                    IsFavourite = false,
                    IsAvailable = true,
                    Category = _categoriesRepository.GetAll().Last()
                },
                new Car
                {
                    Name = "BMW M3",
                    ShortDescription = "Дерзкий и стильный",
                    LongDescription = "Удобный автомобиль для городской жизни",
                    ImageUrl = "https://cs.copart.com/v1/AUTH_svc.pdoc00001/PIX191/e6939d73-a7ca-4718-a55e-621df407f6ef.JPG",
                    Price = 65000,
                    IsFavourite = true,
                    IsAvailable = true,
                    Category = _categoriesRepository.GetAll().Last()
                },
                new Car
                {
                    Name = "Mercedes C class",
                    ShortDescription = "Уютный и большой",
                    LongDescription = "Удобный автомобиль для городской жизни",
                    ImageUrl = "/img/mercedes.jpeg",
                    Price = 40000,
                    IsFavourite = false,
                    IsAvailable = false,
                    Category = _categoriesRepository.GetAll().Last()
                },
                new Car
                {
                    Name = "Nissan Leaf",
                    ShortDescription = "Бесшумный и экономный",
                    LongDescription = "Удобный автомобиль для городской жизни",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5b/2018_Nissan_Leaf_Tekna_Front.jpg/1920px-2018_Nissan_Leaf_Tekna_Front.jpg",
                    Price = 14000,
                    IsFavourite = true,
                    IsAvailable = true,
                    Category = _categoriesRepository.GetAll().First()
                }
            };
        }

        public void SetAll( IEnumerable<Car> cars )
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Car> GetFavourite()
        {
            throw new System.NotImplementedException();
        }

        public void SetFavourite( IEnumerable<Car> cars )
        {
            throw new System.NotImplementedException();
        }
    }
}