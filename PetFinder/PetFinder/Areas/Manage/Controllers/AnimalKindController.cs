using Microsoft.AspNetCore.Mvc;
using PetFinder.DAL;
using PetFinder.Models;
using System;
using System.Linq;

namespace PetFinder.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AnimalKindController : Controller
    {
        private readonly AppDbContext _context;

        public AnimalKindController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null, bool? status = null)
        {
            var animalKinds = _context.AnimalKinds.AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
                animalKinds = animalKinds.Where(x => x.Name.ToUpper().Contains(search.ToUpper()));

            if (status != null)
                animalKinds = animalKinds.Where(x => x.IsDeleted == status);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(animalKinds.Count() / 8d);

            return View(animalKinds.Skip((page - 1) * 8).Take(8).ToList());


        }

        public IActionResult Recycle(int id)
        {
            AnimalKind animalKind = _context.AnimalKinds.FirstOrDefault(x => x.Id == id);
            animalKind.IsDeleted = false;
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
        public IActionResult Create(AnimalKind animalKind)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_context.AnimalKinds.Any(x => x.Name == animalKind.Name))
            {
                ModelState.AddModelError("Name", "Bu rəng artıq mövcuddur!");
                return View();
            }
            _context.AnimalKinds.Add(animalKind);
            animalKind.CreatedAt = DateTime.UtcNow.AddHours(4);
            animalKind.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            AnimalKind animalKind = _context.AnimalKinds.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (animalKind == null)
                return RedirectToAction("error", "dashboard");

            return View(animalKind);
        }

        [HttpPost]
        public IActionResult Edit(AnimalKind animalKind)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.AnimalKinds.Any(x => x.Name == animalKind.Name))
            {
                ModelState.AddModelError("Name", "Bu rəng artıq mövcuddur!");
                return View();
            }
            AnimalKind existAnimalKind = _context.AnimalKinds.FirstOrDefault(x => x.Id == animalKind.Id && !x.IsDeleted);

            if (existAnimalKind == null)
                return RedirectToAction("error", "dashboard");

            existAnimalKind.Name = animalKind.Name;
            existAnimalKind.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            AnimalKind animalKind = _context.AnimalKinds.FirstOrDefault(x => x.Id == id);
            AnimalKind deletedAnimalKind = _context.AnimalKinds.FirstOrDefault(x => x.IsDeleted == true && x.Id == id);

            if (animalKind == null)
                return NotFound();
            animalKind.ModifiedAt = DateTime.UtcNow.AddHours(4);
            animalKind.IsDeleted = true;
            if (deletedAnimalKind != null)
            {
                _context.AnimalKinds.Remove(deletedAnimalKind);
            }

            _context.SaveChanges();

            return Ok();
        }

    }
}

