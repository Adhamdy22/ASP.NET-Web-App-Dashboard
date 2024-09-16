using Managers;
using Managers.Categories;
using Managers.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Runtime.Serialization;
using ViewModel.Items;
namespace Project_1.Areas.Items.Controllers
{
    [Authorize(Roles = Roles.roleProducts + "," + Roles.roleAdmin)]
    [Area("Items")]
    public class ItemsController : Controller
    {
        private readonly AppDbContext _db;
        private ItemsManager _itemsManager;
        private CategoryManager categoryManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        public ItemsController(ItemsManager itemsManager,CategoryManager _categoryManager,IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _itemsManager = itemsManager ?? throw new ArgumentNullException(nameof(itemsManager));
            categoryManager =_categoryManager;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public IActionResult ViewItem(int id)
        {
            var item = _itemsManager.GetById(id); // Fetch item by ID
            if (item == null)
            {
                return NotFound();
            }
            var viewModel = new AddItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CategoryId = item.CategoryId,
                ImagePath = item.ImagePath,
                CreatedDate = item.CreatedDate
            };

            // Populate the category list
            //createSelectList();
            createSelectList(item.CategoryId);
            return View(viewModel);
        }

        //GET
        public IActionResult AddNewItem()
        {
            createSelectList();
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewItem(AddItemViewModel item)
        {
            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                // Store the user ID in the CreatedBy field
                var userid = user?.Id;
                var newItem = new Item
                {
                    Name = item.Name,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    CreatedBy= userid,
                    //ImagePath=item.ImagePath
                    // map other properties as needed
                };

                string uniqueFileName = null;

                // Handle image upload
                if (item.ItemImage != null)
                {
                    // Set the folder path for saving the image
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    // Generate a unique file name to avoid overwriting existing files
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + item.ItemImage.FileName;

                    // Combine folder path and unique file name to form the file path
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file on the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.ItemImage.CopyToAsync(fileStream);
                    }

                    // Save the relative image path to the database
                    newItem.ImagePath = "/images/" + uniqueFileName;
                }
                //// File upload processing
                //if (item.ImagePath != null && item.ImagePath.Length > 0)
                //{
                //    // Set the folder path for saving the image
                //    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                //    // Create a unique file name to avoid overwriting existing files
                //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + item.ImagePath.FileName;

                //    // Combine folder path and unique file name to form the file path
                //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //    // Save the file on the server
                //    using (var fileStream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await item.ImagePath.CopyToAsync(fileStream);
                //    }

                //    // Save the image path (relative) to the database
                //    newItem.ImagePath = "/images/" + uniqueFileName;
                //}

                _itemsManager.Add(newItem);
                TempData["successData"] = "Item has been added successfully";
                return RedirectToAction("Index", "Home", new { area = "Items" });
            }
            else
            {
                return View(item);
            }
        }

        public void createSelectList(int selectId = 1)
        {
            //var categories = _db.Categories.ToList();
            //SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            //ViewBag.CategoryList = listItems;
            var categories = categoryManager.GetAll().ToList();
                SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
                ViewBag.CategoryList = listItems;
        }

        //GET

        [HttpGet]
        public IActionResult EditItem(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var item = _itemsManager.GetById(Id);
            if (item == null)
            {
                return NotFound();
            }

            var itemViewModel = new AddItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CategoryId = item.CategoryId,
                ImagePath = item.ImagePath // Store the current image path
            };

            // Populate category list
            createSelectList(item.CategoryId);

            return View(itemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(AddItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = new SelectList(categoryManager.GetAll(), "Id", "Name", model.CategoryId);
                return View(model);
            }

            // Retrieve the existing item from the database
            var existItem = _itemsManager.GetById(model.Id);
            if (existItem == null)
            {
                return NotFound();
            }

            // Check if a new image is uploaded
            if (model.ItemImage != null && model.ItemImage.Length > 0)
            {
                // Generate a unique file name for the new image
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ItemImage.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                // Ensure the folder exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Full path to save the file
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the new image file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ItemImage.CopyTo(fileStream);
                }

                // Optionally delete the old image if a new one is uploaded
                if (!string.IsNullOrEmpty(existItem.ImagePath))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existItem.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Update the item with the new image path
                existItem.ImagePath = "/images/" + uniqueFileName;  // Update path relative to wwwroot
            }

            // Update other fields
            existItem.Name = model.Name;
            existItem.Price = model.Price;
            existItem.CategoryId = model.CategoryId;

            // Save changes to the item
            _itemsManager.Update(existItem);

            TempData["successData"] = "Item has been updated successfully";
            return RedirectToAction("Index", "Home", new { area = "Items" });
        }







        //GET
        public IActionResult DeleteItem(int id)
        {
            var item = _itemsManager.GetById(id); // Fetch item by ID
            if (item == null)
            {
                return NotFound();
            }

            // Map the existing item to AddItemViewModel
            var itemViewModel = new AddItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CategoryId = item.CategoryId,
                ImagePath = item.ImagePath
            };

           
                    // Populate the category list
                    //createSelectList();
            createSelectList(item.CategoryId);
            return View(itemViewModel);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(AddItemViewModel item)
        {
            if (item == null)
            {
                return NotFound();
            }

            var existItem = _itemsManager.GetById(item.Id);
            if (existItem == null)
            {
                return NotFound();
            }




            // Delete the existing item
            _itemsManager.Delete(existItem);

            TempData["successData"] = "Item has been deleted successfully";
            return RedirectToAction("Index", "Home", new { area = "Items" });
        }
    }
}