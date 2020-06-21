using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Utility;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public string PhoneNumber { get; set; }
        }
        public async Task<IActionResult> OnGet(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return Page();

            var userFromDb = await _db.ApplicationUser.FindAsync(Id);

            Input = new InputModel()
            {
                Id = userFromDb.Id,
                Name = userFromDb.Name,
                Email = userFromDb.Email,
                Address = userFromDb.Address,
                City = userFromDb.City,
                PostalCode = userFromDb.PostalCode,
                PhoneNumber = userFromDb.PhoneNumber
            };

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            if(Input.Id == null)
            {
                return Page();
            }

            var userFromDb = await _db.ApplicationUser.FindAsync(Input.Id);
            _db.ApplicationUser.Remove(userFromDb);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}