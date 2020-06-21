using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models.ViewModel;

namespace SparkAuto.Pages.Cars
{
    public class IndexModel : PageModel
    {
        public ApplicationDbContext _db { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CarAndCustomerViewModel CarAndCustomerViewModel { get; set; }
        public async Task<IActionResult> OnGet(string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            CarAndCustomerViewModel = new CarAndCustomerViewModel()
            {
                Cars = await _db.Car.Where(u => u.UserId == userId).ToListAsync(),
                UserObj = await _db.ApplicationUser.FirstOrDefaultAsync(x => x.Id == userId),

            };

            return Page();
        }
    }
}