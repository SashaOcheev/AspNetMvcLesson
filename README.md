# AspNetMvcLesson
ASP NET MVC Introduction

Для работы необходимы следующие компоненты:
- VisualStudio 2019
- Разработка классических приложений .net
- ASP.NET и разработка веб-приложений

## I - Создание сайта на C#

#### 1. Введение. Что такое ASP.NET
ASP NET - платформа для разработки Web-проектов. Поддерживает несколько языков. C# в том числе.  
Применение: простейшие ресурсы и сложные сайты с большим количеством пользователей.  
На технологии написаны: stackoverflow, mdsn, dell официальный сайт  
![Схема MVC](https://itproger.com/img/courses/asp_net_2.jpg)

.NET Framework - Windows only  
.NET Core - кроссплатформа - наш выбор  

#### 2. Создание проекта
Мы будем создавать магазин автомобилей  
a) Создаем Solution с названием **Shop**  
b) Веб-приложение ASP.NET Core  
c) Выбираем шаблон "Пустой проект"  

## II - Настройки проекта

#### 1. Обзор того, что уже есть
ConnectedServices, Properties, launchSettings.json и т.п.  
Program.cs -> Main() - Точка входа в программу. Здесь мы создаем Веб-хост  
Startup.cs - инфраструктурные вещи, настройка окружения  

**Запускаем**

#### 2. Добавляем библиотеки для работы
С помощью __NuGet__ (рассказать чуть-чуть) ставим следующие либы:
- Microsoft.AspNetCore.StaticFiles (Изображения, стили)
- AspNet.AspNetCore.Mvc.Core (для работы с шаблоном MVC)

#### 3. Конфигурим Startup
Startup.cs
- ConfigureServices() для регистрации модулей
```diff
public void ConfigureServices( IServiceCollection services )
{
+	services.AddMvc();
}
```
- Confgire() для конфигурации модулей
```diff
public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
{
+	app.UseDeveloperExceptionPage(); // чтобы видеть ошибки
+	app.UseStatusCodePages(); // отображать код запроса
+	app.UseStaticFiles(); // отображать css, картинки и прочее
+	app.UseRouting(); // Настроить маршрутизацию
+	app.UseEndpoints( endpoints =>
+	{
+		endpoints.MapControllerRoute(
+			name: "default",
+			pattern: "{controller}/{action}"
+		);
+	} );
}
  ```

## III - Создание моделей и интерфейсов в ASP.NET

#### 1. Модель данных
В нашем случае - место, где мы храним информацию. У нас это магазин

#### 2. Создание моделей внутри приложения
Создать:  
a) {ProjRoot}/Data - папка для работы с бизнем-моделью  
b) {ProjRoot}/Data/Models - сюда будем складывать модели  
c) {ProjRoot}/Data/Models/Category.cs - Категории (Электрические, ДВС)  
```
public class Category
{
    public string Name { get; set; }
    public string Description { get; set; }
}
```
d) {ProjRoot}/Data/Models/Car.cs - Товары (автомобили)
```
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
```

#### 3. Сервисы для работы с данными - репозитории
Создать:  
a) {ProjRoot}/Data/Interfaces  
b) {ProjRoot}/Data/Interfaces/ICategoriesRepository.cs  
```
public interface ICategoriesRepository
{
    IEnumerable<Category> GetAll();
}
```

c) {ProjRoot}/Data/Interfaces/ICarsRepository.cs  
```
public interface ICarsRepository
{
    IEnumerable<Car> GetAll();
    void SetAll( IEnumerable<Car> cars );
    IEnumerable<Car> GetFavourite();
    void SetFavourite( IEnumerable<Car> cars );
}
```

d) {ProjRoot}/Data/Repositories - реализации  
e) {ProjRoot}/Data/CategoriesRepository.cs
```
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
```
f) {ProjRoot}/Data/CarsRepository.cs
```
public class CarsRepository : ICarsRepository
{
    private readonly ICategoriesRepository _categoriesRepository = new CategoriesRepository();

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
                ImageUrl = "https://avatars.mds.yandex.net/get-autoru-vos/2154462/3604b76fe8a853aab75dfd8abf0d9d11/456x342",
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
                ImageUrl = "https://www.mercedes-benz.ru/passengercars/mercedes-benz-cars/models/c-class/saloon-w205/offers-and-services/offer/_jcr_content/backgroundimage.MQ6.12.20200213103356.jpeg",
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
```
## IV - Создание контроллеров и HTML шаблонов

#### 1. Добавляем биндинги
a) Startup.ConfigureServices(): добавляем сопоставления интерфейсов и их реализаций:
```diff
public void ConfigureServices( IServiceCollection services )
{
	services.AddMvc();
+	services.AddScoped<ICarsRepository, CarsRepository>();
+	services.AddScoped<ICategoriesRepository, CategoriesRepository>();
}
```
b) Теперь мы можем не создавать CategoriesRepository через new в CarsRepositories.  
Объявим конструктор с необходимыми зависимостями, а фреймворк сам прокинет нужный экземпляр
```diff
public class CarsRepository : ICarsRepository
{
-   private readonly ICategoriesRepository _categoriesRepository = new CategoriesRepository();
+   private readonly ICategoriesRepository _categoriesRepository;

+    public CarsRepository( ICategoriesRepository categoriesRepository )
+    {
+        _categoriesRepository = categoriesRepository;
+    }
```

#### 2. Добавляем контроллер
a) {ProjRoot}/Controllers  
b) {ProjRoot}/Controllers/CarsController.cs  
Здесб возвращается ViewResult - HTML страничка  
```
using Microsoft.AspNetCore.Mvc;
...
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
```

#### 3. Создаем шаблон
Создать:  
a) {ProjRoot}/Views - здесь лежат HTML шаблоны  
b) {ProjRoot}/Views/Cars - здесь будут искаться шаблоны для CarsController  
c) {ProjRoot}/Views/Cars/CarsList.cshtml - Сам шаблон Empty do not select "Use a layout page"  
```
@using Shop.Data.Models
@model IEnumerable<Car>
@{ 
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width">
    <title>List</title>
</head>
<body>
    <h1>Все автомобили</h1>
    @{ 
        foreach(var car in Model)
        {
            <div>
                <h2>Модель: @car.Name</h2>
                <p>Цена: @car.Price.ToString("c")</p><!--ToString("c") - "c" значит формат отображения денежный-->
            </div>
        }
    }
</body>
</html>
```

#### 4. Смотрим результат
a) Запускаем проект в режиме отладки в конфигурации IIS Express  
b) Добавляем в адрес ресурc: /Cars/CarList  
c) ?????  
d) PROFIT  

В дальнейшем мы будем часто запускать проект в режиме отладки, и добавлять /Cars/CarList всякий раз неудобно.  
Чтобы у нас сразу открывалась нужная страница, можно прописать необходимые параметры в {ProjRoot}/Porperties/aunchSettings.json.  
Поскольку мы запускаем проект в конфигурации IIS Express, то это секцию и правим:  
```diff
...
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
+     "launchUrl": "cars/list"
    },
    "Shop": {
...
```

#### 5. ViewBag
a) Добавить ViewBag в метод CarList():
```diff
...
public ViewResult CarsList()
{
+   ViewBag.Category = "Some New"; // Вместо Category может быть что угодно. Но, сем меньше таких штук, тем лучше
    var cars = _carsRepository.GetAll();
    return View( cars );
}
...
```
b) Добавить ViewBag во View
```diff
...
<body>
    <h1>Все автомобили</h1>
+   <h3>@ViewBag.Category</h3>
    @{ 
        foreach(var car in Model)
...
```

#### 6. ViewModels
Создать:  
a) {ProjRoot}/ViewModels  
b) {ProjRoot}/ViewModels/CarsListViewModel.cs - Это только для шаблонов, здесь мы передаем сразу все данные  
```
public class CarsListViewModel
{
    public IEnumerable<Car> AllCars { get; set; }
    public string CurrentCategory { get; set; }
}
```
c) Добавить новый метод List() в CarsController. Здесь диффом показано отличие от метода CarsList()
```diff
public ViewResult List()
{
    CarsListViewModel carsViewModel = new CarsListViewModel();
+   carsViewModel.AllCars = _carsRepository.GetAll();
+   carsViewModel.CurrentCategory = "Автомобили";
    return View( carsViewModel );
}
```
d) Добавить вьюху для нового метода: {ProjRoot}/Views/Cars/List.cshtml. Здесь диффом показано отличие от CarsList.cshtml
```diff
+@using Shop.ViewModels
+@model CarsListViewModel
@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width">
    <title>List</title>
</head>
<body>
    <h1>Все автомобили</h1>
+   <h3>@Model.CurrentCategory</h3>
    @{
+       foreach ( var car in Model.AllCars )
        {
            <div>
                <h2>Модель: @car.Name</h2>
                <p>Цена: @car.Price.ToString( "c" )</p><!--ToString("c") - "c" значит формат отображения денежный-->
            </div>
        }
    }
</body>
</html>
```
e) Также для удобства можно поменять launchUrl в launchSettings.json на "cars/list"
#### 7. Добавление Layout
Layout - шаблон, который является основой для других шаблонов
Создать:
a) {ProjRoot}/Views/Shared - название обязательно должно быть Shared  
b) В Shared добавить следующим образом:  
Add Item: Web->ASP.NET RazorLayout -> __Layout.cshtml_. (Лэйауты принято начинать нижним подчеркиванием)
```
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
</head>
<body>
    <div>
        @RenderBody()
    </div>
</body>
</html>
```
с) Изменить List.cshml: указать Layout и удлалить код, который уже есть в Layout
```diff
@using Shop.ViewModels
@model CarsListViewModel
@{
+   Layout = "_Layout";
}

-<!DOCTYPE html>

-<html>
-<head>
-   <meta name="viewport" content="width=device-width">
-   <title>List</title>
-</head>
-<body>
<h1>Все автомобили</h1>
<h3>@Model.CurrentCategory</h3>
@{
    foreach ( var car in Model.AllCars )
    {
        <div>
            <h2>Модель: @car.Name</h2>
            <p>Цена: @car.Price.ToString( "c" )</p><!--ToString("c") - "c" значит формат отображения денежный-->
        </div>
    }
}
-</body>
-</html>
```
#### 8. Добавим ViewStart
Во ViewStart мы пропишем Layout и он (Layout) будет выставляться везде автоматически
a) Добавить {ProjectRoot}/Views/_ViewStart.cshtml (шаблон _Razor View Start_)
```
@{
    Layout = "_Layout";
}
```
b) Удалить из шаблона List.cshtml Layout
```diff
...
@model CarsListViewModel
-@{
-   Layout = "_Layout";
-}

<h1>Все автомобили</h1>
<h3>@Model.CurrentCategory</h3>
...
```
#### 9. Добавим Razor View Imports
Для того, чтобы не прописывать импорты вверху шаблона: 
a) Добавить {ProjectRoot}/Views/_ViewImports.cshtml (шаблон _Razor View Imports_)
```
@using Shop.ViewModels
```
```diff
b) Удалить импорты иp List.cshtml

-@using Shop.ViewModels
-@model CarsListViewModel

<h1>Все автомобили</h1>
<h3>@Model.CurrentCategory</h3>
@{
...
```

#### 10. Задать ViewBag.Title
В layout мы видим вот что
`<title>@ViewBag.Title</title>`
Но Title мы никак не передаем  
a) Обновим CarsController.List()
```diff
public ViewResult List()
{
+   ViewBag.Title = "Страница с автомобилями"; // В данном случае передача задумана через ViewBag
    CarsListViewModel carsViewModel = new CarsListViewModel();
    carsViewModel.AllCars = _carsRepository.GetAll();
    carsViewModel.CurrentCategory = "Автомобили";
    return View( carsViewModel );
}
```
## V - Добавление Bootstrap и фото на сайт
#### 1. Добавляем Bootstrap
- Bootstrap можно скачать и добавить в проект (наш случай)
- Можно в Layout добавить ссылку
- Через NuGet  
a) Создаем папки для статики (картинки, css, js и т.п):  
- {ProjRoot}/wwwroot - здесь проект будет искать статику автоматически  
- {ProjRoot}/img - для картинок  
- {ProjRoot}/css - для стилкей  
- {ProjRoot}/js - для javascript-файлов  
b) Заходим на https://getbootstrap.com/ , скачиваем архив, распкавовыаем его  
- закидываем оттуда bootstrap.min.css в {ProjRoot}/wwwroot/css  
- аналогичные действия проделываем с js-файлом  

#### 2. Подключаем стили
a) Обновляем Layout
```diff
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
+   <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css">
</head>
<body>
    <div>
        @RenderBody()
    </div>
+   <script src="~/js/bootstrap.min.js">
</body>
</html>
```
b) Добавляем свой файл стилей в style.css (Add Item -> ASP.NET Core -> StyleSheet)
```
body {
    background: #fcfcfc;
}
```
Подключем файл в наш Layout
```diff
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css">
+   <link href="~/css/style.css" rel="stylesheet" type="text/css">
</head>
```
#### 3. Закидываем картинки
a) Я скачал пару картинок для mersedec и для ford (адреса картинок взял из CarsRepository). Дальше закинул их в {ProjDir}/wwwroot/img  
b) Подключаем картинуи в CarsRepository:
```diff
...
new Car
{
    Name = "Ford Fiesta",
    ShortDescription = "Тихий и спокойный",
    LongDescription = "Удобный автомобиль для городской жизни",
+   ImageUrl = "/img/ford.webp",
    Price = 11000,
    IsFavourite = false,
    IsAvailable = true,
    Category = _categoriesRepository.GetAll().Last()
},
...
new Car
{
    Name = "Mercedes C class",
    ShortDescription = "Уютный и большой",
    LongDescription = "Удобный автомобиль для городской жизни",
+   ImageUrl = "/img/mercedes.jpeg",
    Price = 40000,
    IsFavourite = false,
    IsAvailable = false,
    Category = _categoriesRepository.GetAll().Last()
},
```
#### 4. Заимствуем дизайн
a) Находим готовые дизайны на getsbootstrap.com. В моем случае это Carousel: https://getbootstrap.com/docs/4.3/examples/carousel/  
b) Берем оттуда header и footer и вставляем в наш Layout
```diff
...
<body>
+   <header>
+       <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
+           <a class="navbar-brand" href="#">Carousel</a>
+           <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
+               <span class="navbar-toggler-icon"></span>
+           </button>
+           <div class="collapse navbar-collapse" id="navbarCollapse">
+               <ul class="navbar-nav mr-auto">
+                   <li class="nav-item active">
+                       <a class="nav-link" href="#">Home <span class="sr-only">(current)</span></a>
+                   </li>
+                   <li class="nav-item">
+                       <a class="nav-link" href="#">Link</a>
+                   </li>
+                   <li class="nav-item">
+                       <a class="nav-link disabled" href="#">Disabled</a>
+                   </li>
+               </ul>
+               <form class="form-inline mt-2 mt-md-0">
+                   <input class="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search">
+                   <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
+               </form>
+           </div>
+       </nav>
+   </header>
    <div>
        @RenderBody()
    </div>
+   <footer class="container">
+       <p class="float-right"><a href="#">Back to top</a></p>
+       <p>&copy; 2017-2019 Company, Inc. &middot; <a href="#">Privacy</a> &middot; <a href="#">Terms</a></p>
+   </footer>
</body>
...
```
c) Обновляем/наполняем List.cshtml
```
<h1>Все автомобили</h1>
<h3>@Model.CurrentCategory</h3>
<div class="row mt-5 mb-2">
	@{
		foreach ( var car in Model.AllCars )
		{
			<div class="col-lg-4">
				<img class="img-thumbnail" src="@car.ImageUrl" alt="@car.Name">
				<h2>@car.Name</h2>
				<p>@car.ShortDescription</p>
				<p>Цена: @car.Price.ToString( "c" )</p><!--ToString("c") - "c" значит формат отображения денежный-->
				<p><a class="btn btn-warning" href="#">Подробнее</a></p>
			</div>
		}
	}
</div>
```
d) У нас спряталась надпись "Все автомобили". Надо бы ее вернуть. Обновим Layout:
```diff
...
    </header>
+   <div class="container mt-5" >
        @RenderBody()
    </div>
    <footer class="container">
...
```
Вся информация взята отсюда: https://itproger.com/course/asp-net. Там есть еще 5 уроков, к каждому уроку видос.
У нас не было работы с формами и отправкой данных на сервер, там все это есть.
