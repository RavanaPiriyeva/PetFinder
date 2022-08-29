using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFinder.DAL;
using PetFinder.Models;
using System;
using System.Linq;

namespace PetFinder.Areas.Manage.Controllers
{
    [Area("manage")]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null, bool? status = null)
        {
            var categorys = _context.Categories.AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
                categorys = categorys.Where(x => x.Name.ToUpper().Contains(search.ToUpper()));

            if (status != null)
                categorys = categorys.Where(x => x.IsDeleted == status);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(categorys.Count() / 8d);

            return View(categorys.Skip((page - 1) * 8).Take(8).ToList());


        }

        public IActionResult Recycle(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            category.IsDeleted = false;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Cancel()
        {
            return RedirectToAction("index");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_context.Categories.Any(x => x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Bu şəhər artıq mövcuddur!");
                return View();
            }
            _context.Categories.Add(category);
            category.CreatedAt = DateTime.UtcNow.AddHours(4);
            category.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null)
                return RedirectToAction("error", "dashboard");

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Categories.Any(x => x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Bu şəhər artıq mövcuddur!");
                return View();
            }
            Category existType = _context.Categories.FirstOrDefault(x => x.Id == category.Id && !x.IsDeleted);

            if (existType == null)
                return RedirectToAction("error", "dashboard");

            existType.Name = category.Name;
            existType.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            Category deletedType = _context.Categories.FirstOrDefault(x => x.IsDeleted == true && x.Id == id);

            if (category == null)
                return NotFound();
            category.ModifiedAt = DateTime.UtcNow.AddHours(4);
            category.IsDeleted = true;
            if (deletedType != null)
            {
                _context.Categories.Remove(deletedType);
            }

            _context.SaveChanges();

            return Ok();
        }

    }
}
