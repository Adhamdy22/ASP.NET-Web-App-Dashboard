using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Managers.Categories;
using Managers.Items;
using ViewModel.Items;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Project_1.Areas.Items.Controllers
{
    [Authorize(Roles = Roles.roleProducts + "," + Roles.roleAdmin)]
    [Area("Items")]
    public class HomeController : Controller
    {
        public ItemsManager itemsManager;

        public CategoryManager categoryManager;

        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ItemsManager items, CategoryManager categoryManager, UserManager<IdentityUser> userManager)
        {
            this.itemsManager = items ?? throw new ArgumentNullException(nameof(items));
            this.categoryManager = categoryManager ?? throw new ArgumentNullException(nameof(categoryManager));
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    IEnumerable<Item> itemsList = _db.Items.Include(c => c.Category).ToList();
        //    return View(itemsList);
        //}
        //public IActionResult Index()
        //{
        //    var onecat = itemsManager.Search(x => x.Category.Name == "Computers");
        //    return View(itemsManager.GetAll().Include(x=>x.Category).ToList());
        //}
        //public async Task<IActionResult> Index(string searchtext,decimal price,int CategoryId)
        //{
        //    // var onecat = itemsManager.Search(x => x.Name == "Robot Car");
        //    var user = await _userManager.GetUserAsync(User);  // Get the logged-in user
        //    var userId = user?.Id;  // Get user Id
        //                            //var allcat=CategoryManager.GetAll();

        //    IQueryable<Item> ItemsQuery = itemsManager.GetAll();
        //    if (!User.IsInRole(Roles.roleAdmin))
        //    {
        //        ItemsQuery = ItemsQuery.Where(c => c.CreatedBy == userId);
        //    }

        //    List<Item> items = ItemsQuery
        //         .Where(x => (string.IsNullOrEmpty(searchtext) || x.Name.Contains(searchtext)) &&
        //                     (price == 0 || x.Price <= price) &&
        //                     (CategoryId == 0 || x.CategoryId == CategoryId))
        //         .Include(x => x.Category)  // Include related Category information
        //         .ToList();

        //    ViewData["Categories"]=categoryManager.GetAll().Select(s=>new SelectListItem(s.Name, s.Id.ToString(), s.Id == CategoryId)).ToList();

        //    ViewData["price"] = price;
        //    ViewData["searchText"] = searchtext;
        //    ViewData["CategoryId"] = CategoryId;


        //    //return View(itemsManager.GetAll().Include(x=>x.Category).ToList());

        //    return View("Index",items);
        //}
        public async Task<IActionResult> Index(string searchtext, decimal price, int CategoryId, string sortColumn = "Id", bool isAscending = true)
        {
            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            // Get all items from itemsManager
            IQueryable<Item> ItemsQuery = itemsManager.GetAll();

            // If user is not an admin, filter items by CreatedBy (the logged-in user's Id)
            if (!User.IsInRole(Roles.roleAdmin))
            {
                ItemsQuery = ItemsQuery.Where(c => c.CreatedBy == userId);
            }

            // Apply sorting using the Sort function from BaseManager<T>
            ItemsQuery = itemsManager.Sort(ItemsQuery, sortColumn, isAscending);

            // Filter based on searchtext, price, and CategoryId
            List<Item> items = ItemsQuery
                .Where(x => (string.IsNullOrEmpty(searchtext) || x.Name.Contains(searchtext)) &&
                            (price == 0 || x.Price <= price) &&
                            (CategoryId == 0 || x.CategoryId == CategoryId))
                .Include(x => x.Category)  // Include related Category information
                .ToList();

            // Populate ViewData with categories and form fields
            ViewData["Categories"] = categoryManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.Id.ToString(), s.Id == CategoryId)).ToList();

            ViewData["price"] = price;
            ViewData["searchText"] = searchtext;
            ViewData["CategoryId"] = CategoryId;

            // Return the view with sorted and filtered items
            return View("Index", items);
        }














        //public async Task<IActionResult> Index(string category = "Computers")
        //{
        //    // Filter items by category
        //    var filteredItems = itemsManager.Search(x => x.Category.Name == category).ToList();

        //    // Return the filtered items to the view
        //    return View(filteredItems);
        //}

    }
}
