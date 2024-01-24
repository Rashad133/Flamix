using Flamix.Areas.Admin.ViewModels;
using Flamix.DAL;
using Flamix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flamix.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _db;
        public ServiceController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _db.Services.ToListAsync();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM create)
        {
            if(!ModelState.IsValid) return View(create);
            bool result = await _db.Services.AnyAsync(x=>x.Name.Trim().ToLower()==create.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name","is exists");
                return View(create);
            }
            Service service = new Service
            { 
                Name = create.Name,
                Description = create.Description,
                Icon= create.Icon,
            };
            await _db.Services.AddAsync(service);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));    
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Service service = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (service == null) return NotFound();
            UpdateServiceVM update = new UpdateServiceVM
            {
                Name = service.Name,
                Description = service.Description,
                Icon = service.Icon,
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateServiceVM update)
        {
            if (!ModelState.IsValid) return View(update);
            Service service = await _db.Services.FirstOrDefaultAsync(x=>x.Id == id);
            if (service == null) return NotFound();

            bool result = await _db.Services.AnyAsync(x=>x.Name.Trim().ToLower()==update.Name.Trim().ToLower() && x.Id!=id);
            if (result)
            {
                ModelState.AddModelError("Name","is exists");
                return View(update);
            }
            service.Name = update.Name;
            service.Description = update.Description;
            service.Icon = update.Icon;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Service service = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (service == null) return NotFound();

            _db.Services.Remove(service);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
            

    }
}
