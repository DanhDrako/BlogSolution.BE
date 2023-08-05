using BlogSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Data.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<UserEntity> Users { get; set; }
        //public DbSet<CategoryEntity> Categories { get; set; }
        //public DbSet<PostEntity> Posts { get; set; }
    }
}
