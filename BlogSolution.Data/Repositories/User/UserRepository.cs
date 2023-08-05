using BlogSolution.Data.Entities;
using BlogSolution.Data.Infrastructure;
using BlogSolution.Data.EF;
using BlogSolution.Data.Repositories.Node;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Data.Repositories.User
{
    public class UserRepository : CommonRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context){}
    }
}
