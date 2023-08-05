using BlogSolution.Data.Entities;
using BlogSolution.Data.Infrastructure;
using BlogSolution.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Data.Repositories.Node
{
    public interface ICategoryRepository : ICommonRepository<CategoryEntity>
    {
    }
}
