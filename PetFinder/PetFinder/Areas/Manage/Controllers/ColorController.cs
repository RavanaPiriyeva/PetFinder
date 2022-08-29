using Microsoft.AspNetCore.Mvc;
using PetFinder.DAL;
using PetFinder.Models;
using System;
using System.Linq;

namespace PetFinder.Areas.Manage.Controllers
{
    [Area("manage")]

    public class ColorController : Controller
    {
       

        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null, bool? status = null)
        {
            var colors = _context.Colors.AsQueryable();
           

            if (!string.IsNullOrWhiteSpace(search))
                colors = colors.Where(x => x.Name.ToUpper().Contains(search.ToUpper()));

            if (status != null)
                colors = colors.Where(x => x.IsDeleted == status);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(colors.Count() / 8d);

            return View(colors.Skip((page - 1) * 8).Take(8).ToList());

            
        }

        public IActionResult Recycle(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id ==id);
            color.IsDeleted = false;
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
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_context.Colors.Any(x => x.Name == color.Name))
            {
                ModelState.AddModelError("Name", "Bu rəng artıq mövcuddur!");
                return View();
            }
            _context.Colors.Add(color);
            color.CreatedAt = DateTime.UtcNow.AddHours(4);
            color.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (color == null)
                return RedirectToAction("error", "dashboard");

            return View(color);
        }

        [HttpPost]
        public IActionResult Edit(Color color)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Colors.Any(x => x.Name == color.Name))
            {
                ModelState.AddModelError("Name", "Bu rəng artıq mövcuddur!");
                return View();
            }
            Color existColor = _context.Colors.FirstOrDefault(x => x.Id == color.Id && !x.IsDeleted);

            if (existColor == null)
                return RedirectToAction("error", "dashboard");

            existColor.Name = color.Name;
            existColor.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);
            Color deletedColor =_context.Colors.FirstOrDefault(x => x.IsDeleted == true && x.Id==id);

            if (color == null)
                return NotFound();
            color.ModifiedAt = DateTime.UtcNow.AddHours(4);
            color.IsDeleted = true;
            if(deletedColor != null)
            {
                _context.Colors.Remove(deletedColor);
            }
             
            _context.SaveChanges();

            return Ok();
        }

    }
}

