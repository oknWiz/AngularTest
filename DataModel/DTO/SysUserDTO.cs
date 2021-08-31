using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataModel.DTO
{
    public partial class SysUserDTO
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RoleName { get; set; }
    }
}
