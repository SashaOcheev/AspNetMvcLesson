using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Data.Interfaces
{
    public interface ICarsRepository
    {
        IEnumerable<Car> GetAll();
        void SetAll( IEnumerable<Car> cars );
        IEnumerable<Car> GetFavourite();
        void SetFavourite( IEnumerable<Car> cars );
    }
}