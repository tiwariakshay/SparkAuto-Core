using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Models.ViewModel
{
    public class UserListViewModel
    {
        public List<ApplicationUser> UserList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
