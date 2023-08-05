using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.ViewModel.User
{
    public class RegisterRequest : LoginRequest
    {
        public string ConfirmPassword { get; set; }
    }
}
