using Shop.Data.Interfaces;
using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Data.Repositories
{
	public class CategoriesRepository : ICategoriesRepository
	{
		public IEnumerable<Category> GetAll()
		{
			return new List<Category>
			{
				new Category
				{
					Name = "Электромобиль",
					Description = "Автомобиль с электродвигателем"
				},
				new Category
				{
					Name = "Классический",
					Description = "Автособиль с ДВС"
				}
			};
		}
	}
}