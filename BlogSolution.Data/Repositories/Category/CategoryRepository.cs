using BlogSolution.Data.EF;
using BlogSolution.Data.Entities;
using BlogSolution.Data.Enums;
using BlogSolution.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogSolution.Data.Repositories.Node
{
    public class CategoryRepository : CommonRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context){}
    }
}
