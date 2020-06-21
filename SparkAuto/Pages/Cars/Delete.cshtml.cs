using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly SparkAuto.Data.ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }


        public DeleteModel(SparkAuto.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await _db.Car.FirstOrDefaultAsync(m => m.Id == id);

            if (Car == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var carFromDb = await _db.Car.FindAsync(Car.Id);
            _db.Car.Remove(carFromDb);
            await _db.SaveChangesAsync();

            StatusMessage = "Car has been deleted successfully";


            return RedirectToPage("Index", new { userId = Car.UserId});
        }

    }
}