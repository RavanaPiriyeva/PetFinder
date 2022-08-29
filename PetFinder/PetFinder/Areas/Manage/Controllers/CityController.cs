using Microsoft.AspNetCore.Mvc;
using PetFinder.DAL;
using PetFinder.Models;
using System;
using System.Linq;

namespace PetFinder.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CityController : Controller
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null, bool? status = null)
        {
            var citys = _context.Cities.AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
                citys = citys.Where(x => x.Name.ToUpper().Contains(search.ToUpper()));

            if (status != null)
                citys = citys.Where(x => x.IsDeleted == status);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(citys.Count() / 8d);

            return View(citys.Skip((page - 1) * 8).Take(8).ToList());


        }

        public IActionResult Recycle(int id)
        {
            City city = _context.Cities.FirstOrDefault(x => x.Id == id);
            city.IsDeleted = false;
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
        public IActionResult Create(City city)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_context.Cities.Any(x => x.Name == city.Name))
            {
                ModelState.AddModelError("Name", "Bu şəhər artıq mövcuddur!");
                return View();
            }
            _context.Cities.Add(city);
            city.CreatedAt = DateTime.UtcNow.AddHours(4);
            city.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            City city = _context.Cities.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (city == null)
                return RedirectToAction("error", "dashboard");

            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(City city)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Cities.Any(x => x.Name == city.Name))
            {
                ModelState.AddModelError("Name", "Bu şəhər artıq mövcuddur!");
                return View();
            }
            City existCity = _context.Cities.FirstOrDefault(x => x.Id == city.Id && !x.IsDeleted);

            if (existCity == null)
                return RedirectToAction("error", "dashboard");

            existCity.Name = city.Name;
            existCity.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            City city = _context.Cities.FirstOrDefault(x => x.Id == id);
            City deletedCity = _context.Cities.FirstOrDefault(x => x.IsDeleted == true && x.Id == id);

            if (city == null)
                return NotFound();
            city.ModifiedAt = DateTime.UtcNow.AddHours(4);
            city.IsDeleted = true;
            if (deletedCity != null)
            {
                _context.Cities.Remove(deletedCity);
            }

            _context.SaveChanges();

            return Ok();
        }

    }

}
