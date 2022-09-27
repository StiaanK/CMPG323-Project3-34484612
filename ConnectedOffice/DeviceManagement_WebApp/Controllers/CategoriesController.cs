using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        // Retrieves all the Category records from DB
        public async Task<IActionResult> Index()
        {
            return View(_categoryRepository.GetAll());
        }

        // Recieve Details of a Category record
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catagory = _categoryRepository.GetById(id);

            if (catagory == null)
            {
                return NotFound();
            }

            return View(catagory);
        }

        // Adds new Category record to DB
        // GET part of Create(Category) 
        public IActionResult Create()
        {
            return View();
        }

        //POST part of Create(Category)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _categoryRepository.Add(category);
            _categoryRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // Edits a Category record on DB
        // GET part of Edit(Category)
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id);
            if (category== null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST part of Edit(Category)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            try
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // Removes Category record from DB
        // GET part of Delete(Category)
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST part of Delete(Category)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            _categoryRepository.Remove(category);
            _categoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // Checks if Category record exists
        private bool CategoryExists(Guid id)
        {
            Category category = _categoryRepository.GetById(id);

            if (category != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
