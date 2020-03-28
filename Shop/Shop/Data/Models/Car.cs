namespace Shop.Data.Models
{
	public class Car
	{
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }
		public string ImageUrl { get; set; }
		public ushort Price { get; set; }
		public bool IsFavourite { get; set; } // отображение на главной
		public bool IsAvailable { get; set; } // доступен ли для продажи
		public Category Category { get; set; }
	}
}