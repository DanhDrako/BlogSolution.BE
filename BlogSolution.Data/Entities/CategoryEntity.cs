using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlogSolution.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using BlogSolution.Utilities.Common;
using BlogSolution.Utilities.Enums;

namespace BlogSolution.Data.Entities
{
    [Table("Category")]
    public class CategoryEntity : CommonEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Status Status { get; set; }

        // Foreign key (1-n)
        public List<PostEntity> Posts { get; set; }
    }
}
