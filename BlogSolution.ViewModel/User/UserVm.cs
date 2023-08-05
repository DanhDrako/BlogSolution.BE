using BlogSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.ViewModel.User
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }
    }
}
