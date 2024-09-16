using Managers;
using Managers.Categories;
using Managers.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models;
using System.Runtime.Serialization;
using ViewModel.Categories;
using ViewModel.Items;
using ViewModel.Roles;
namespace Project_1.Areas.Categories.Controllers
{

    [Authorize(Roles = Roles.roleCategory + "," + Roles.roleAdmin)]
    [Area("Categories")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        private CategoryManager categoryManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public CategoryController(CategoryManager _categoryManager,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            categoryManager = _categoryManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult ViewCategory(int id)
        {
            var item = categoryManager.GetById(id); // Fetch item by ID
            if (item == null)
            {
                return NotFound();
            }

            // Populate the category list
            //createSelectList();
            //createSelectList(item.CategoryId);
            return View(item);
        }

        //GET
        public IActionResult AddNewCategory()
        {
            //createSelectList();
            
            

            return View();
            //return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = Roles.roleCategory)]
        public  async Task<IActionResult> AddNewCategory(AddCategoryViewModel category)
        {
            // Additional validation example (e.g., name length)
            if (category.Name.Length < 3)
            {
                ModelState.AddModelError("Name", "Category name must be at least 3 characters long.");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                // Store the user ID in the CreatedBy field
                var userid = user?.Id;

                var newCategory = new Category
                {
                    Name = category.Name,
                    CreatedBy = userid, // Assign the signed-in user's ID to CreatedBy
                    Items = category.Items
                    // Add other properties if needed
                };

                // Add to database (assuming categoryManager handles it correctly)
                try
                {
                    categoryManager.Add(newCategory);
                    TempData["successData"] = "Category has been added successfully";
                    return RedirectToAction("Index", "Home", new { area = "Categories" });
                }
                catch (Exception ex)
                {
                    // Catch any errors during saving
                    ModelState.AddModelError("", $"An error occurred while saving the category: {ex.Message}");
                }
            }

            
            // If validation fails, return the view with the existing data and errors
            return View(category);
        }


        //public void createSelectList(int selectId = 1)
        //{
        //    //var categories = _db.Categories.ToList();
        //    //SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
        //    //ViewBag.CategoryList = listItems;
        //    var categories = categoryManager.GetAll().ToList();
        //    SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
        //    ViewBag.CategoryList = listItems;
        //}

        //GET
        public IActionResult EditCategory(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var category = categoryManager.GetById(Id);
            if (category == null)
            {
                return NotFound();
            }

            // Populate category list
            //createSelectList(item.CategoryId);

            return View(category);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(AddCategoryViewModel category)
        {
            if (category.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }

            if (ModelState.IsValid)
            {
                var existitem = categoryManager.GetById(category.Id);

                if (existitem == null)
                {
                    TempData["ErrorData"] = "Category not Found";
                    return RedirectToAction("Index", "Home", new { area = "Categories" });
                }
                existitem.Name = category.Name;
                // map other properties as needed
                categoryManager.Update(existitem);
                TempData["successData"] = "Category has been updated successfully";
                return RedirectToAction("Index", "Home", new { area = "Categories" });
            }
            else
            {
                // Re-populate the category list for the view
                //createSelectList(item.CategoryId);
                return View(category);
            }
        }


        //GET
        public IActionResult DeleteCategory(int id)
        {
            var item = categoryManager.GetById(id); // Fetch item by ID
            if (item == null)
            {
                return NotFound();
            }

            // Populate the category list
            //createSelectList();
            //createSelectList(item.CategoryId);
            return View(item);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(AddCategoryViewModel item)
        {
            if (item == null)
            {
                return NotFound();
            }

            var existItem = categoryManager.GetById(item.Id);
            if (existItem == null)
            {
                return NotFound();
            }

            // Delete the existing item
            categoryManager.Delete(existItem);

            TempData["successData"] = "Category has been deleted successfully";
            return RedirectToAction("Index", "Home", new { area = "Categories" });
        }
    }
}
