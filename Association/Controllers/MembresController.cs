using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Association.Models;
using Microsoft.AspNetCore.Hosting;
using Association.ViewModels;
using System.IO;

namespace Association.Controllers
{
    public class MembresController : Controller
    {
        private readonly AppDBContext _context;
        private readonly AppDBContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MembresController(AppDBContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> New1()
        {
            var membre = await dbContext.Membres.ToListAsync();
            return View(membre);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(MembreViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Membre membre = new Membre
                {
                    Prenom = model.Prenom,
                    Nom = model.Nom,
                    Sexe = model.Sexe,
                    Data_Naiss = model.Data_Naiss,
                    Lieu_Naiss = model.Lieu_Naiss,
                    Nationalite = model.Nationalite,
                    Num_telephone = model.Num_telephone,
                    Adresse = model.Adresse,
                    Profession = model.Profession,
                    ProfilePicture = uniqueFileName,
                };
                dbContext.Add(membre);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(New1));
            }
            return View();
        }

        private string UploadedFile(MembreViewModel model)
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








        // GET: Membres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Membres.ToListAsync());
        }

        // GET: Membres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // GET: Membres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Membres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prenom,Nom,Sexe,Data_Naiss,Lieu_Naiss,Nationalite,Num_telephone,Adresse,Profession,ProfilePicture")] Membre membre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(New1));
            }
            return View(membre);
        }

        // GET: Membres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
            {
                return NotFound();
            }
            return View(membre);
        }

        // POST: Membres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prenom,Nom,Sexe,Data_Naiss,Lieu_Naiss,Nationalite,Num_telephone,Adresse,Profession,ProfilePicture")] Membre membre)
        {
            if (id != membre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembreExists(membre.Id))
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
            return View(membre);
        }

        // GET: Membres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // POST: Membres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membre = await _context.Membres.FindAsync(id);
            _context.Membres.Remove(membre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(New1));
        }

        private bool MembreExists(int id)
        {
            return _context.Membres.Any(e => e.Id == id);
        }
    }
}
