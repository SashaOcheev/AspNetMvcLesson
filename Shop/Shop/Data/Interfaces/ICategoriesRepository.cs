using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Data.Intefaces
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> GetAll();
    }
}