using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Association.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Association.ViewModel;

namespace Association.Controllers
{
    public class AssociationsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly AppDBContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AssociationsController(AppDBContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> New1()
        {
            var association = await dbContext.Associations.ToListAsync();
            return View(association);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(AssociationViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Association1 association = new Association1
                {
                    Nom = model.Nom,
                    Code = model.Code,
                    Annee_creation = model.Annee_creation,
                    Telephone1 = model.Telephone1,
                    Telephone2 = model.Telephone2,
                    Email = model.Email,
                    Adresse = model.Adresse,
                    ProfilePicture = uniqueFileName,
                };
                dbContext.Add(association);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(New1));
            }
            return View();
        }

        private string UploadedFile(AssociationViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }




        // GET: Associations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Associations.ToListAsync());
        }

        // GET: Associations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Associations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (association == null)
            {
                return NotFound();
            }

            return View(association);
        }

        // GET: Associations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Associations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Code,Annee_creation,Telephone1,Telephone2,Email,Adresse,ProfilePicture")] Association1 association)
        {
            if (ModelState.IsValid)
            {
                _context.Add(association);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(New1));
            }
            return View(association);
        }

        // GET: Associations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Associations.FindAsync(id);
            if (association == null)
            {
                return NotFound();
            }
            return View(association);
        }

        // POST: Associations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Code,Annee_creation,Telephone1,Telephone2,Email,Adresse,ProfilePicture")] Association1 association)
        {
            if (id != association.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(association);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociationExists(association.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(New1));
            }
            return View(association);
        }

        // GET: Associations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Associations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (association == null)
            {
                return NotFound();
            }

            return View(association);
        }

        // POST: Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var association = await _context.Associations.FindAsync(id);
            _context.Associations.Remove(association);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(New1));
        }

        private bool AssociationExists(int id)
        {
            return _context.Associations.Any(e => e.Id == id);
        }
    }
}
