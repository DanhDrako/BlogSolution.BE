using BlogSolution.Utilities.Common;
using BlogSolution.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Application.User
{
    public interface IUserService
    {
        public Task<BaseResultModel> Login(LoginRequest user);
        public Task<BaseResultModel> Register(RegisterRequest user);
        public Task<BaseResultModel> GetInfoCurrent();

    }
}
