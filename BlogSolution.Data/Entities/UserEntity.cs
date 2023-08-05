using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSolution.Utilities.Common;
using BlogSolution.Data.Enums;
using BlogSolution.Utilities.Enums;

namespace BlogSolution.Data.Entities
{
    [Table("User")]
    public class UserEntity : CommonEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public string LinkFacebook { get; set; }

        [Required]
        public UserRole Role { get; set; }
        public string Avatar { get; set; }
        public string AvatarUploadType { get; set; }
        public string AccessToken { get; set; }
        public UserStatus Status { get; set; }
        public bool IsOnline { get; set; }
        public string LastLogin { get; set; }
        public string LastIp { get; set; }


    }
}
