using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFinder.DAL;
using PetFinder.Models;
using System;
using System.Linq;

namespace PetFinder.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ShelterController : Controller
    {
        private readonly AppDbContext _context;

        public ShelterController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null, bool? status = null)
        {
            var shelters = _context.Shelters.Include(x=> x.City).AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
                shelters = shelters.Where(x => x.Name.ToUpper().Contains(search.ToUpper()));

            if (status != null)
                shelters = shelters.Where(x => x.IsDeleted == status);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(shelters.Count() / 8d);

            return View(shelters.Skip((page - 1) * 8).Take(8).ToList());


        }

        public IActionResult Recycle(int id)
        {
            Shelter shelter = _context.Shelters.FirstOrDefault(x => x.Id == id);
            shelter.IsDeleted = false;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Cancel()
        {
            return RedirectToAction("index");
        }
        public IActionResult Create()
        {
            ViewBag.City = _context.Cities.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Shelter shelter)
        {
            if (!_context.Cities.Any(x => x.Id == shelter.CityId))
                ModelState.AddModelError("CityId", "Şəhər tapılmadı");

            if (!ModelState.IsValid)
            {
                ViewBag.City = _context.Cities.ToList();

                return View();
            }

           

            if (_context.Shelters.Any(x => x.Name == shelter.Name))
            {
                ModelState.AddModelError("Name", "Bu rəng artıq mövcuddur!");
                return View();
            }
            _context.Shelters.Add(shelter);
            shelter.CreatedAt = DateTime.UtcNow.AddHours(4);
            shelter.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            Shelter shelter = _context.Shelters.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (shelter == null)
                return RedirectToAction("error", "dashboard");

            ViewBag.City = _context.Cities.ToList();
            return View(shelter);
        }

        [HttpPost]
        public IActionResult Edit(Shelter shelter)
        {
            if (!ModelState.IsValid)
            return View();

            ViewBag.City = _context.Cities.ToList();

            if (_context.Shelters.Any(x => x.Name == shelter.Name))
            {
                ModelState.AddModelError("Name", "Bu rəng artıq mövcuddur!");
                return View();
            }
            Shelter existShelter = _context.Shelters.FirstOrDefault(x => x.Id == shelter.Id && !x.IsDeleted);


            if (existShelter == null)
                return RedirectToAction("error", "dashboard");

            if (existShelter.CityId != shelter.CityId && !_context.Shelters.Any(x => x.Id == shelter.CityId))
                ModelState.AddModelError("GenreId", "Genre notfound");
            existShelter.Name = shelter.Name;
            existShelter.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Shelter shelter = _context.Shelters.FirstOrDefault(x => x.Id == id);
            Shelter deletedShelter = _context.Shelters.FirstOrDefault(x => x.IsDeleted == true && x.Id == id);

            if (shelter == null)
                return NotFound();
            shelter.ModifiedAt = DateTime.UtcNow.AddHours(4);
            shelter.IsDeleted = true;
            if (deletedShelter != null)
            {
                _context.Shelters.Remove(deletedShelter);
            }

            _context.SaveChanges();

            return Ok();
        }

    }
}

