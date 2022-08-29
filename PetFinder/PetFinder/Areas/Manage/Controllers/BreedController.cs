using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFinder.DAL;
using PetFinder.Models;
using System;
using System.Linq;

namespace PetFinder.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BreedController : Controller
    {
        private readonly AppDbContext _context;

        public BreedController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null, bool? status = null)
        {
            var breeds = _context.Breeds.Include(x => x.AnimalKind).AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
                breeds = breeds.Where(x => x.Name.ToUpper().Contains(search.ToUpper()));

            if (status != null)
                breeds = breeds.Where(x => x.IsDeleted == status);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(breeds.Count() / 8d);

            return View(breeds.Skip((page - 1) * 8).Take(8).ToList());


        }

        public IActionResult Recycle(int id)
        {
            Breed breed = _context.Breeds.FirstOrDefault(x => x.Id == id);
            breed.IsDeleted = false;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Cancel()
        {
            return RedirectToAction("index");
        }
        public IActionResult Create()
        {
            ViewBag.AnimalKind = _context.AnimalKinds.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Breed breed)
        {
            if (!_context.AnimalKinds.Any(x => x.Id == breed.AnimalKindId))
                ModelState.AddModelError("AnimalKindId", "Ev heyavanı tapılmadı");

            if (!ModelState.IsValid)
            {
                ViewBag.AnimalKind = _context.AnimalKinds.ToList();

                return View();
            }



            if (_context.Breeds.Any(x => x.Name == breed.Name))
            {
                ModelState.AddModelError("Name", "Bu rəng artıq mövcuddur!");
                return View();
            }
            _context.Breeds.Add(breed);
            breed.CreatedAt = DateTime.UtcNow.AddHours(4);
            breed.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            Breed breed = _context.Breeds.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (breed == null)
                return RedirectToAction("error", "dashboard");

            ViewBag.AnimalKind = _context.AnimalKinds.ToList();
            return View(breed);
        }

        [HttpPost]
        public IActionResult Edit(Breed breed)
        {
            if (!ModelState.IsValid)
                return View();

            ViewBag.AnimalKind = _context.AnimalKinds.ToList();

            if (_context.Breeds.Any(x => x.Name == breed.Name && x.AnimalKind == breed.AnimalKind))
            {
                ModelState.AddModelError("Name", "Bu cins  artıq mövcuddur!");
                return View();
            }
            Breed existBreed = _context.Breeds.FirstOrDefault(x => x.Id == breed.Id && !x.IsDeleted);


            if (existBreed == null)
                return RedirectToAction("error", "dashboard");

            if (existBreed.AnimalKindId != breed.AnimalKindId && !_context.Breeds.Any(x => x.Id == breed.AnimalKindId))
                ModelState.AddModelError("GenreId", "Genre notfound");
            existBreed.Name = breed.Name;
            existBreed.AnimalKindId = breed.AnimalKindId;
            existBreed.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Breed breed = _context.Breeds.FirstOrDefault(x => x.Id == id);
            Breed deletedBreed = _context.Breeds.FirstOrDefault(x => x.IsDeleted == true && x.Id == id);

            if (breed == null)
                return NotFound();
            breed.ModifiedAt = DateTime.UtcNow.AddHours(4);
            breed.IsDeleted = true;
            if (deletedBreed != null)
            {
                _context.Breeds.Remove(deletedBreed);
            }

            _context.SaveChanges();

            return Ok();
        }

    }
}
