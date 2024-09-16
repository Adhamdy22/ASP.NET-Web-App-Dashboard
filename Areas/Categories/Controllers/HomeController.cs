using Managers;
using Managers.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Project_1.Areas.Categories.Controllers
{

    //[Authorize(Roles =Roles.roleCategory)]
    [Authorize(Roles = $"{Roles.roleCategory}, {Roles.roleAdmin} , {Roles.roleProducts}")]
    [Area("Categories")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(CategoryManager categorymanager, UserManager<IdentityUser> userManager)
        {
            CategoryManager = categorymanager;  // Correct assignment
            _userManager = userManager;
        }
        public CategoryManager CategoryManager { get; set; }
        //public IActionResult Index()
        //{
        //    return View(CategoryManager.GetAll());
        //}

        //[Authorize(Roles = Roles.roleCategory)]
        //[Authorize(Roles = Roles.roleAdmin)]
       
        public async Task<IActionResult> Index()
        {
            //var onecat = CategoryManager.Search(x => x.Name == "Computers");

            var user = await _userManager.GetUserAsync(User);  // Get the logged-in user
            var userId = user?.Id;  // Get user Id
                                    //var allcat=CategoryManager.GetAll();

            IQueryable<Category> categoriesQuery = CategoryManager.GetAll();

            if (User.IsInRole(Roles.roleCategory))
            {
                categoriesQuery = categoriesQuery.Where(c => c.CreatedBy == userId);
            }

            var categories = categoriesQuery.ToList();
            return View(categories);

           // return View(allcat);
        }


    }
}
