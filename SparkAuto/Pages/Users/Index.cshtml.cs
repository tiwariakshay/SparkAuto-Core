using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Models.ViewModel;
using SparkAuto.Utility;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public UserListViewModel UserListViewModel { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGet(int productPage = 1, string searchEmail = null, string searchName = null, string searchPhone = null)
        {
            UserListViewModel = new UserListViewModel()
            {
                UserList = await _db.ApplicationUser.ToListAsync()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Users?productPage=:");
            param.Append("&searchName=");
            if (!string.IsNullOrEmpty(searchName))
            {
                param.Append(searchName);
            }

            param.Append("&searchEmail=");
            if (!string.IsNullOrEmpty(searchEmail))
            {
                param.Append(searchEmail);
            }

            param.Append("&searchPhone=");
            if (!string.IsNullOrEmpty(searchPhone))
            {
                param.Append(searchPhone);
            }

            var count = UserListViewModel.UserList.Count;

            if (!string.IsNullOrEmpty(searchEmail))
            {
                UserListViewModel.UserList = await _db.ApplicationUser.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower())).ToListAsync();
            }
            else
            {
                if (!string.IsNullOrEmpty(searchName))
                {
                    UserListViewModel.UserList = await _db.ApplicationUser.Where(u => u.Name.ToLower().Contains(searchName.ToLower())).ToListAsync();
                }
                else
                {
                    if (!string.IsNullOrEmpty(searchPhone))
                    {
                        UserListViewModel.UserList = await _db.ApplicationUser.Where(u => u.PhoneNumber.ToLower().Contains(searchPhone.ToLower())).ToListAsync();
                    }
                }
            }

            UserListViewModel.PagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                TotalItems = count,
                ItemsPerPage = StaticDetails.PaginationUsersPage,
                UrlParam = param.ToString()
            };

            UserListViewModel.UserList = UserListViewModel.UserList.OrderBy(p => p.Email).Skip((productPage - 1) * StaticDetails.PaginationUsersPage)
                .Take(StaticDetails.PaginationUsersPage).ToList();

            return Page();
        }


    }
}