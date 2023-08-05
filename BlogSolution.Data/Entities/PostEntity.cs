using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSolution.Utilities.Common;

namespace BlogSolution.Data.Entities
{
    [Table("Post")]
    public class PostEntity : CommonEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        // Foreign key (1-n)
        public int CateId { get; set; }
        // Navigation property
        [ForeignKey("CateId")]
        public CategoryEntity Category { get; set; }
    }
}
