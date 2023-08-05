﻿using BlogSolution.Data.Entities;
using BlogSolution.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Data.Repositories.User
{
    public interface IUserRepository : ICommonRepository<UserEntity>
    {
    }
}
